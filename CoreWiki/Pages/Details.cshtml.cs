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
using System.Linq;
using System.Security.Claims;

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

		public ArticleDetailsDTO Article { get; set; }

		[ViewDataAttribute]
		public string Slug { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

			// TODO: If topicName not specified, default to Home Page

			slug = slug ?? UrlHelpers.HomePageSlug;

			var article = await _articleRepo.GetArticleBySlug(slug);

			if (article == null)
			{
				Slug = slug;
				var historical = await _slugHistoryRepo.GetSlugHistoryWithArticle(slug);

				if (historical != null)
				{
					return new RedirectResult($"~/wiki/{historical.Article.Slug}");
				}
				else
				{
					return new ArticleNotFoundResult(slug);
				}
			}

			var comments = (
				from comment in article.Comments
				select new CommentDTO
				{
					ArticleId = comment.Id,
					DisplayName = comment.DisplayName,
					Email = comment.Email,
					Content = comment.Content,
					Submitted = comment.Submitted
				}
			).ToList();

			Article = new ArticleDetailsDTO
			{
				Id = article.Id,
				AuthorId = article.AuthorId,
				Slug = article.Slug,
				Topic = article.Topic,
				Content = article.Content,
				Published = article.Published,
				Version = article.Version,
				ViewCount = article.ViewCount,
				Comments = comments
			};

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

		public async Task<IActionResult> OnPostAsync(CommentDTO dto)
		{
			TryValidateModel(dto);

			var comment = new Comment
			{
				IdArticle = dto.ArticleId,
				Content = dto.Content,
				DisplayName = dto.DisplayName,
				Email = dto.Email,
				AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
			};

			var article = await _articleRepo.GetArticleByComment(comment);
			if (article == null)
				return new ArticleNotFoundResult();

			if (!ModelState.IsValid)
				return Page();

			comment.Article = article;
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
