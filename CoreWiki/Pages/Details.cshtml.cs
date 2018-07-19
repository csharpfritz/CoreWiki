using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Data.Security;
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

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly IArticleRepository _articleRepo;
		private readonly ICommentRepository _commentRepo;
		private readonly ISlugHistoryRepository _slugHistoryRepo;
		private readonly IClock _clock;
		private readonly UserManager<CoreWikiUser> _UserManager;
		private readonly INotificationService _notificationService;

		public IConfiguration Configuration { get; }
		public IEmailSender Notifier { get; }

		public DetailsModel(
			IArticleRepository articleRepo,
			ICommentRepository commentRepo,
			ISlugHistoryRepository slugHistoryRepo,
			UserManager<CoreWikiUser> userManager,
			IConfiguration config,
			INotificationService notificationService,
			IClock clock)
		{
			_articleRepo = articleRepo;
			_commentRepo = commentRepo;
			_slugHistoryRepo = slugHistoryRepo;
			_clock = clock;
			_UserManager = userManager;
			_notificationService = notificationService;
			Configuration = config;
		}

		public Article Article { get; set; }

		[ViewDataAttribute]
		public string Slug { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

			// TODO: If topicName not specified, default to Home Page

			slug = slug ?? "home-page";
			Article = await _articleRepo.GetArticleBySlug(slug);

			if (Article == null)
			{
				Slug = slug;
				var historical = await _slugHistoryRepo.GetSlugHistoryWithArticle(slug);

				if (historical != null)
				{
					return new RedirectResult($"~/{historical.Article.Slug}");
				}
				else
				{
					return new ArticleNotFoundResult(slug);
				}
			}

			if (Request.Cookies[Article.Topic] == null)
			{
				Article.ViewCount++;
				Response.Cookies.Append(Article.Topic, "foo", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.UtcNow.AddMinutes(5)
				});
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(Comment comment)
		{
			TryValidateModel(comment);
			Article = await _articleRepo.GetArticleByComment(comment);
			if (Article == null)
				return new ArticleNotFoundResult();

			if (!ModelState.IsValid)
				return Page();

			comment.Article = Article;

			comment.Submitted = _clock.GetCurrentInstant();

			var author = await _UserManager.FindByIdAsync(Article.AuthorId.ToString());
			await _commentRepo.CreateComment(comment);

			// TODO: Also check for verified email if required
			await _notificationService.SendNewCommentEmail(author.Email, author.UserName, comment.DisplayName, Article.Topic, Article.Slug, () => author.CanNotify);

			return Redirect($"/{Article.Slug}");
		}
	}
}
