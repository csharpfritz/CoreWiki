using System;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Events;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Application.Common;
using CoreWiki.Areas.Identity;
using CoreWiki.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoreWiki.Pages
{
	[Authorize(PolicyConstants.CanDeleteArticles)]
	public class DeleteModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public DeleteModel(IMediator mediator, IMapper mapper, ILogger logger)
		{
			_mediator = mediator;
			_mapper = mapper;
			_logger = logger;
		}

		[BindProperty]
		public ArticleDelete Article { get; set; }

		///  TODO: Make it so you cannot delete the home page (deleting the home page will cause a 404)
		///  or re-factor to make the home page dynamic or configurable.
		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}
			if (slug.Equals(Constants.HomePageSlug, StringComparison.InvariantCultureIgnoreCase))
			{
				_logger.LogError("User tried to delete Homepage");
				await _mediator.Publish(new DeleteHomePageAttemptNotification());
				return Forbid();
			}

			var article = await _mediator.Send(new GetArticleQuery(slug));

			if (article == null)
			{
				return NotFound();
			}

			Article = _mapper.Map<ArticleDelete>(article);

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var result = await _mediator.Send(new DeleteArticleCommand(slug));
			return LocalRedirect(Constants.HomePageUrl);
		}
	}
}
