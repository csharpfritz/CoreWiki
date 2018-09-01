using CoreWiki.Core.Interfaces;
using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Application.Helpers;
using MediatR;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Articles.Commands;
using System;
using System.Security.Claims;

namespace CoreWiki.Pages
{
	[Authorize]
	public class CreateArticleFromLinkModel : PageModel
	{
		[BindProperty]
		public ArticleCreateFromLink Article { get; set; }
		[BindProperty]
		public List<string> LinksToCreate { get; set; } = new List<string>();
		public IMediator Mediator { get; }

		public CreateArticleFromLinkModel(IMediator mediator)
		{
			this.Mediator = mediator;
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var theArticle = await Mediator.Send(new GetArticle(id));
			if (theArticle == null)
			{
				return new ArticleNotFoundResult();
			}

			LinksToCreate = (await Mediator.Send(new GetArticlesToCreateFromArticle(id))).ToList();
			if (LinksToCreate.Count == 0)
			{
				return Redirect($"/wiki/{(theArticle.Slug == UrlHelpers.HomePageSlug ? "" : theArticle.Slug)}");
			}

			Article = new ArticleCreateFromLink
			{
				Slug = theArticle.Slug
			};

			return Page();
		}

		public async Task<IActionResult> OnPostCreateLinksAsync(string slug)
		{

			var taskList = new List<Task>();

			Parallel.ForEach(LinksToCreate, link =>
			{

				var createCmd = new CreateNewArticleCommand(
					topic: link.ToTitleCase().RemoveHyphens(),
					slug: link,
					content: string.Empty,
					authorId: Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
					authorName: User.Identity.Name
				);

				taskList.Add(Mediator.Send(createCmd));

			});

			Task.WaitAll(taskList.ToArray());

			return Redirect($"/wiki/{(slug == UrlHelpers.HomePageSlug ? "" : slug)}");
		}

		public IActionResult OnPostCancel(string slug)
		{
			return Redirect($"/wiki/{(slug == UrlHelpers.HomePageSlug ? "" : slug)}");
		}
	}
}
