using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using CoreWiki.Application.Articles.Reading.Commands;
using CoreWiki.Application.Articles.Reading.Queries;
using CoreWiki.Application.Common;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public DetailsModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public ArticleDetails Article { get; set; }

		[ViewData]
		public string Slug { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			slug = slug ?? Constants.HomePageSlug;
			var article = await _mediator.Send(new GetArticleQuery(slug));

			if (article == null)
			{
				var historical = await _mediator.Send(new GetSlugHistoryQuery(slug));

				if (historical != null)
				{
					return Redirect(historical.Article.Slug);
				}

				return new ArticleNotFoundResult(slug);
			}

			Article = _mapper.Map<ArticleDetails>(article); 

			ManageViewCount(slug);

			return Page();
		}

		private void ManageViewCount(string slug)
		{
			var incrementViewCount = (Request.Cookies[slug] == null);
			if (!incrementViewCount)
			{
				return;
			}

			Article.ViewCount++;
			Response.Cookies.Append(slug, "foo", new Microsoft.AspNetCore.Http.CookieOptions
			{
				Expires = DateTime.UtcNow.AddMinutes(5)
			});

			_mediator.Send(new IncrementViewCountCommand(slug));
		}

		public async Task<IActionResult> OnPostAsync(Comment model)
		{
			TryValidateModel(model);

			if (!ModelState.IsValid)
				return Page();

			var article = await _mediator.Send(new GetArticleByIdQuery(model.ArticleId));

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			var commentCmd = _mapper.Map<CreateNewCommentCommand>(model);
				commentCmd = _mapper.Map(User, commentCmd);

			await _mediator.Send(commentCmd);

			return Redirect(article.Slug);
		}
	}
}
