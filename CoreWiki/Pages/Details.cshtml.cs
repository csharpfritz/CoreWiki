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
using CoreWiki.Application.Articles.Notifications;
using System.Threading;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public IEmailSender Notifier { get; }

		public DetailsModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
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

			var article = await _mediator.Send(new GetArticleById(dto.ArticleId));

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			var comment = _mapper.Map<Core.Domain.Comment>(dto);
			comment.AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

			var result = await _mediator.Send(new CreateNewCommentCommand(article, comment));

			return Redirect($"/wiki/{article.Slug}");
		}
	}

	// TODO: Move this to a more suitable location in the project
	public class NewCommentNotificationHandler : INotificationHandler<CommentPostedNotification>
	{
		private readonly UserManager<CoreWikiUser> _userManager;
		private readonly INotificationService _notificationService;

		public NewCommentNotificationHandler(UserManager<CoreWikiUser> userManager, INotificationService notificationService)
		{
			_userManager = userManager;
			_notificationService = notificationService;
		}

		public async Task Handle(CommentPostedNotification notification, CancellationToken cancellationToken)
		{
			var author = await _userManager.FindByIdAsync(notification.Article.AuthorId.ToString());
			await _notificationService.SendNewCommentEmail(author.Email, author.UserName, notification.Comment.DisplayName, notification.Article.Topic, notification.Article.Slug, () => author.CanNotify);
		}
	}
}
