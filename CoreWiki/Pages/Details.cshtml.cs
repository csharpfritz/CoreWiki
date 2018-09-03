using CoreWiki.ViewModels;
using CoreWiki.Data.EntityFramework.Security;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWiki.Core.Notifications;
using MediatR;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Articles.Commands;
using AutoMapper;
using CoreWiki.Application.Articles.Notifications;
using System.Threading;
using CoreWiki.Application.Helpers;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

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
			var article = await _mediator.Send(new GetArticleQuery(slug));

			if (article == null)
			{

				var historical = await _mediator.Send(new GetSlugHistory(slug));

				if (historical != null)
				{
					return new RedirectResult($"~/wiki/{historical.Article.Slug}");
				}
				return new ArticleNotFoundResult(slug);
			}

			Article = _mapper.Map<ArticleDetails>(article); 

			ManageViewCount(slug);

			return Page();
		}

		private void ManageViewCount(string slug)
		{
			var incrementViewCount = (Request.Cookies[slug] == null);
			if (!incrementViewCount)
			{
				return;
			}

			Article.ViewCount++;
			Response.Cookies.Append(slug, "foo", new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.UtcNow.AddMinutes(5)
			});
			_mediator.Send(new IncrementViewCount(slug));
		}

		public async Task<IActionResult> OnPostAsync(Comment model)
		{

			TryValidateModel(model);

			if (!ModelState.IsValid)
				return Page();

			var article = await _mediator.Send(new GetArticleByIdQuery(model.ArticleId));

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			var commentCmd = _mapper.Map<CreateNewCommentCommand>(model);
			commentCmd.AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); //todo: use automapper resolver

			await _mediator.Send(commentCmd);

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
			if (author != null)
			{
				await _notificationService.SendNewCommentEmail(author.Email, author.UserName, notification.Comment.DisplayName, notification.Article.Topic, notification.Article.Slug, () => author.CanNotify);
			}
		}
	}
}
