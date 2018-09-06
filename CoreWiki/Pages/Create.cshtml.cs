using CoreWiki.Areas.Identity;
using CoreWiki.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Application.Common;
using CoreWiki.Helpers;

namespace CoreWiki.Pages
{

	[Authorize(Policy = PolicyConstants.CanWriteArticles)]
	public class CreateModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public CreateModel(IMediator mediator, IMapper mapper, ILoggerFactory loggerFactory)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = loggerFactory.CreateLogger("CreatePage");
		}

		public async Task<IActionResult> OnGetAsync(string slug = "")
		{
			if (string.IsNullOrEmpty(slug))
			{
				return Page();
			}

			var request = new GetArticleQuery(slug);
			var result = await _mediator.Send(request);
			if (result == null)
			{
				Article = new ArticleCreate
				{
					Topic = UrlHelpers.SlugToTopic(slug)
				};
			}
			else
			{
				return Redirect($"/{slug}/Edit");
			}

			return Page();
		}

		[BindProperty]
		public ArticleCreate Article { get; set; }   

		public async Task<IActionResult> OnPostAsync()
		{
			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (string.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			if (!ModelState.IsValid) { return Page(); }

			_logger.LogWarning($"Creating page with slug: {slug}");

			var isTopicAvailable = new GetIsTopicAvailableQuery {Slug = slug, ArticleId = 0};
			if (await _mediator.Send(isTopicAvailable))
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
				return Page();
			}

			var cmd = _mapper.Map<CreateNewArticleCommand>(Article);
			cmd = _mapper.Map(User, cmd);

			var cmdResult = await _mediator.Send(cmd);

			// TODO: Inspect result to ensure it ran properly

			var query = new GetArticlesToCreateFromArticleQuery(slug);
			var listOfSlugs = await _mediator.Send(query);

			if (listOfSlugs.Any())
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{slug}");

		}
	}
}
