using CoreWiki.Core.Interfaces;
using CoreWiki.ViewModels;
using CoreWiki.Data.EntityFramework.Security;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NodaTime;
using System;
using System.Threading.Tasks;
using CoreWiki.Core.Notifications;
using System.Linq;
using System.Security.Claims;
using CoreWiki.Core.Domain;
using MediatR;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Articles.Commands;
using AutoMapper;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly IArticleRepository _articleRepo;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ICommentRepository _commentRepo;
		private readonly ISlugHistoryRepository _slugHistoryRepo;
		private readonly IClock _clock;
		private readonly UserManager<CoreWikiUser> _UserManager;
		private readonly INotificationService _notificationService;

		public IEmailSender Notifier { get; }

		public DetailsModel(
			IMediator mediator,
			IMapper mapper,
			ICommentRepository commentRepo,
			ISlugHistoryRepository slugHistoryRepo,
			UserManager<CoreWikiUser> userManager,
			INotificationService notificationService,
			IClock clock)
		{
			_mediator = mediator;
			_mapper = mapper;
			_commentRepo = commentRepo;
			_slugHistoryRepo = slugHistoryRepo;
			_clock = clock;
			_UserManager = userManager;
			_notificationService = notificationService;
		}

		public ArticleDetails Article { get; set; }

		[ViewDataAttribute]
		public string Slug { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

			slug = slug ?? UrlHelpers.HomePageSlug;
			var article = await _mediator.Send(new GetArticle(slug));

			if (article == null)
			{

				var historical = await _mediator.Send(new GetSlugHistory(slug));

				if (historical != null)
				{
					return new RedirectResult($"~/wiki/{historical.Article.Slug}");
				}
				else
				{
					return new ArticleNotFoundResult(slug);
				}
			}

			Article = _mapper.Map<ArticleDetails>(article); 

			ManageViewCount(slug);

			return Page();
		}

		private void ManageViewCount(string slug)
		{
			var incrementViewCount = (Request.Cookies[slug] == null);
			if (incrementViewCount)
			{
				Article.ViewCount++;
				Response.Cookies.Append(slug, "foo", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.UtcNow.AddMinutes(5)
				});
				_mediator.Send(new IncrementViewCount(slug));
			}
		}

		public async Task<IActionResult> OnPostAsync(ViewModels.Comment dto)
		{

			TryValidateModel(dto);

			if (!ModelState.IsValid)
				return Page();

			var comment = dto.ToDomain(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

			var article = await _articleRepo.GetArticleByComment(comment);
			if (article == null)
				return new ArticleNotFoundResult();

			comment.IdArticle = article.Id;
			comment.Submitted = _clock.GetCurrentInstant();
			await _commentRepo.CreateComment(comment);

			// IDEA: Make this an extensibility module, we should only be creating a comment here (single responsibility principle)
			// TODO: Also check for verified email if required
			var author = await _UserManager.FindByIdAsync(article.AuthorId.ToString());
			await _notificationService.SendNewCommentEmail(author.Email, author.UserName, comment.DisplayName, article.Topic, article.Slug, () => author.CanNotify);

			return Redirect($"/wiki/{article.Slug}");
		}
	}
}
