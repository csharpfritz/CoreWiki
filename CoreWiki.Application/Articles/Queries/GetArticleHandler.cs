using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Services;
using CoreWiki.Application.Articles.Services.Dto;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticleHandler
		: IRequestHandler<GetArticleQuery, ArticleReadingDto>,
			IRequestHandler<GetArticleByIdQuery, ArticleReadingDto>,
			IRequestHandler<GetIsTopicAvailableQuery, bool>,
			IRequestHandler<GetSlugHistory, SlugHistoryDto>,
			IRequestHandler<GetArticleWithHistoriesBySlug, ArticleReadingDto>,
			IRequestHandler<GetLatestArticles, List<ArticleReadingDto>>,
			IRequestHandler<SearchArticles, SearchResult<ArticleReadingDto>>
	{

		private readonly IArticleReadingService _articleReadingService;
		private readonly IArticlesSearchEngine _articlesSearchEngine;

		public GetArticleHandler(IArticleReadingService articleReadingService, IArticlesSearchEngine articlesSearchEngine)
		{
			_articleReadingService = articleReadingService;
			_articlesSearchEngine = articlesSearchEngine;
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

		public Task<SlugHistoryDto> Handle(GetSlugHistory request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetSlugHistoryWithArticle(request.HistoricalSlug);
		}

		public Task<ArticleReadingDto> Handle(GetArticleWithHistoriesBySlug request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetArticleWithHistoriesBySlug(request.Slug);
		}

		public Task<List<ArticleReadingDto>> Handle(GetLatestArticles request, CancellationToken cancellationToken)
		{
			return _articleReadingService.GetLatestArticles(request.NumOfArticlesToGet);
		}

		public Task<SearchResult<ArticleReadingDto>> Handle(SearchArticles request, CancellationToken cancellationToken)
		{
			return _articlesSearchEngine.SearchAsync(request.Query, request.PageNumber, request.ResultsPerPage);
		}
	}
}
