﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Helpers;
using CoreWiki.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
				return Redirect(theArticle.GetUrlOrHome());
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
				var createCmd = new CreateSkeletonArticleCommand();
				createCmd = _mapper.Map<CreateSkeletonArticleCommand>(User);
				createCmd.Slug = link;
				taskList.Add(_mediator.Send(createCmd));
			});

			Task.WaitAll(taskList.ToArray());

			return Redirect(ArticleUrlHelpers.GetUrlOrHome(slug));
		}

		public IActionResult OnPostCancel(string slug)
		{
			return Redirect(ArticleUrlHelpers.GetUrlOrHome(slug));
		}
	}
}
