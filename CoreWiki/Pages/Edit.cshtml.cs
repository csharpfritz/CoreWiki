using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWiki.Application.Helpers;
using MediatR;
using AutoMapper;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Queries;
using Microsoft.AspNetCore.Authorization;
using CoreWiki.Areas.Identity;

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

			var cmd = new EditArticleCommand(Article.Id, Article.Topic, Article.Content, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), User.Identity.Name);
			var result = await _mediator.Send(cmd);

			if (result.Exception is InvalidTopicException)
			{
				ModelState.AddModelError("Article.Topic", result.Exception.Message);
				return Page();
			} else if (result.Exception is ArticleNotFoundException)
			{
				return new ArticleNotFoundResult();
			}

			var query = new GetArticlesToCreateFromArticleQuery(UrlHelpers.URLFriendly(Article.Topic));
			var listOfSlugs = await _mediator.Send(query);

			if (listOfSlugs.Any())
			{
				return RedirectToPage("CreateArticleFromLink", new { id = UrlHelpers.URLFriendly(Article.Topic) });
			}

			return Redirect($"/wiki/{(UrlHelpers.URLFriendly(Article.Topic) == UrlHelpers.HomePageSlug ? "" : UrlHelpers.URLFriendly(Article.Topic))}");

		}

		
	}

}
