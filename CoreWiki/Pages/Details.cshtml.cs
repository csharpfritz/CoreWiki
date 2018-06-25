using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;
using NodaTime;
using CoreWiki.Helpers;
using CoreWiki.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity;
using CoreWiki.Areas.Identity.Data;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;
		private readonly IClock _clock;
		private readonly INotificationService _notificationService;

		public DetailsModel(CoreWiki.Models.ApplicationDbContext context, IClock clock, INotificationService notificationService)
		{
			_context = context;
			_clock = clock;
			_notificationService = notificationService;
		}
		private readonly UserManager<CoreWikiUser> _UserManager;

		public IConfiguration Configuration { get; }

		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

            // TODO: If topicName not specified, default to Home Page

            slug = slug ?? "home-page";

			Article = await _context.Articles.Include(x => x.Comments).SingleOrDefaultAsync(m => m.Slug == slug.ToLower());

			if (Article == null)
			{
				return new ArticleNotFoundResult(slug);
			}

			if (Request.Cookies[Article.Topic] == null)
			{
				Article.ViewCount++;
				Response.Cookies.Append(Article.Topic, "foo", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.UtcNow.AddMinutes(5)
				});

				await _context.SaveChangesAsync();
			}

			return Page();
	}

		public async Task<IActionResult> OnPostAsync(Models.Comment comment)
		{
			TryValidateModel(comment);
			Article = await _context.Articles.Include(x => x.Comments).SingleOrDefaultAsync(m => m.Id == comment.IdArticle);

			if (Article == null)
				return new ArticleNotFoundResult();

			if (!ModelState.IsValid)
				return Page();

			comment.Article = this.Article;

			comment.Submitted = _clock.GetCurrentInstant();

			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();
			await _notificationService.NotifyAuthorNewComment(Article, comment);

			return Redirect($"/{Article.Slug}");
		}
	}
}
