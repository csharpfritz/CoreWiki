﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Exceptions;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Areas.Identity;
using CoreWiki.Helpers;
using CoreWiki.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWiki.Pages
{
	[Authorize(Policy = PolicyConstants.CanEditArticles)]
	public class EditModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public EditModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[BindProperty]
		public ArticleEdit Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var article = await _mediator.Send(new GetArticleQuery(slug));

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			Article = _mapper.Map<ArticleEdit>(article);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var cmd = _mapper.Map<EditArticleCommand>(Article);
			cmd = _mapper.Map(User, cmd);

			var result = await _mediator.Send(cmd);

			if (result.Exception is InvalidTopicException)
			{
				ModelState.AddModelError("Article.Topic", result.Exception.Message);
				return Page();
			}
			else if (result.Exception is ArticleNotFoundException)
			{
				return new ArticleNotFoundResult();
			}

			var query = new GetArticlesToCreateFromArticleQuery(Article.Slug);
			var listOfSlugs = await _mediator.Send(query);

			if (listOfSlugs.Any())
			{
				return RedirectToPage("CreateArticleFromLink", new { id = Article.Slug });
			}

			return Redirect(ArticleUrlHelpers.GetUrlOrHome(Article.Slug));
		}
	}
}
