using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System;
using System.Security.Claims;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Application.Common;
using CoreWiki.Core.Common;

namespace CoreWiki.Pages
{
	[Authorize]
	public class CreateArticleFromLinkModel : PageModel
	{
		[BindProperty]
		public ArticleCreateFromLink Article { get; set; }
		[BindProperty]
		public List<string> LinksToCreate { get; set; } = new List<string>();

		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public CreateArticleFromLinkModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var theArticle = await _mediator.Send(new GetArticleQuery(id));
			if (theArticle == null)
			{
				return new ArticleNotFoundResult();
			}

			LinksToCreate = (await _mediator.Send(new GetArticlesToCreateFromArticleQuery(id))).ToList();
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
				var createCmd = _mapper.Map<CreateNewArticleCommand>(link);
				createCmd= _mapper.Map(User, createCmd);
				taskList.Add(_mediator.Send(createCmd));
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
