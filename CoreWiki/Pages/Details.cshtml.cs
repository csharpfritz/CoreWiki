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
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Identity;
using CoreWiki.Areas.Identity.Data;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using CoreWiki.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;
		private readonly IClock _clock;
		private readonly UserManager<CoreWikiUser> _UserManager;

		public IConfiguration Configuration { get; }
		public IEmailSender Notifier { get; }

		public DetailsModel(CoreWiki.Models.ApplicationDbContext context, UserManager<CoreWikiUser> userManager,
			IConfiguration config, IEmailSender notifier,
			IClock clock)
		{
			_context = context;
			_clock = clock;
			_UserManager = userManager;
			this.Configuration = config;
			this.Notifier = notifier;
		}

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

			//Add notifications here:
			var author = await _UserManager.FindByIdAsync(Article.AuthorId.ToString());

			if (author.CanNotify)
			{
				var authorEmail = author.Email;
				// TODO: Verify that we found an author

				var thisUrl = Request.GetEncodedUrl();
				await Notifier.SendEmailAsync(authorEmail, "You have a new comment!", $"Someone said something about your article at {thisUrl}");
			}

			return Redirect($"/{Article.Slug}");
		}
	}
}
