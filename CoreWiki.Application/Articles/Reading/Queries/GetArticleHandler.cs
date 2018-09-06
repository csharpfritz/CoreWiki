using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetArticleHandler
		: IRequestHandler<GetArticleQuery, ArticleReadingDto>,
			IRequestHandler<GetArticleByIdQuery, ArticleReadingDto>,
			IRequestHandler<GetIsTopicAvailableQuery, bool>,
			IRequestHandler<GetSlugHistoryQuery, SlugHistoryDto>,
			IRequestHandler<GetArticleWithHistoriesBySlugQuery, ArticleReadingDto>,
			IRequestHandler<GetLatestArticlesQuery, List<ArticleReadingDto>>
	{

		private readonly IArticleReadingService _articleReadingService;

		public GetArticleHandler(IArticleReadingService articleReadingService)
		{
			_articleReadingService = articleReadingService;
		}

		public Task<ArticleReadingDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetArticleBySlug(request.Slug);
		}

		public Task<ArticleReadingDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetArticleById(request.Id);
		}

		public Task<bool> Handle(GetIsTopicAvailableQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.IsTopicAvailable(request.Slug, request.ArticleId);
		}

		public Task<SlugHistoryDto> Handle(GetSlugHistoryQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetSlugHistoryWithArticle(request.HistoricalSlug);
		}

		public Task<ArticleReadingDto> Handle(GetArticleWithHistoriesBySlugQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetArticleWithHistoriesBySlug(request.Slug);
		}

		public Task<List<ArticleReadingDto>> Handle(GetLatestArticlesQuery request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetLatestArticles(request.NumOfArticlesToGet);
		}
	}
}
