using CoreWiki.Data;
using CoreWiki.Core.Interfaces;
using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWiki.Data.EntityFramework;
using CoreWiki.Application.Helpers;
using MediatR;
using AutoMapper;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Application.Articles.Exceptions;

namespace CoreWiki.Pages
{

	public class EditModel : PageModel
	{

		private readonly IMediator _Mediator;
		private readonly IMapper _Mapper;

		private readonly IArticleRepository _Repo;
		private readonly ISlugHistoryRepository _SlugRepo;
		private readonly IClock _clock;

		public EditModel(IMediator mediator, IMapper mapper)
		{
			_Mediator = mediator;
			_Mapper = mapper;
		}

		[BindProperty]
		public ArticleEdit Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var article = await _Mediator.Send(new GetArticle(slug));

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			Article = _Mapper.Map<ArticleEdit>(article);

			return Page();

		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var cmd = new EditArticleCommand(Article.Id, Article.Topic, Article.Content, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), User.Identity.Name);
			var result = await _Mediator.Send(cmd);

			if (result.Exception is InvalidTopicException)
			{
				ModelState.AddModelError("Article.Topic", result.Exception.Message);
				return Page();
			} else if (result.Exception is ArticleNotFoundException)
			{
				return new ArticleNotFoundResult();
			}

			var slug = UrlHelpers.URLFriendly(Article.Topic);
			var query = new GetArticlesToCreateFromArticle(slug);
			var listOfSlugs = await _Mediator.Send(query);

			if (listOfSlugs.Any())
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{(slug == UrlHelpers.HomePageSlug ? "" : slug)}");

		}

		
	}

}
