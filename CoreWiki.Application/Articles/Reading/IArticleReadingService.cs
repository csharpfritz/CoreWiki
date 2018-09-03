using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Dto;

namespace CoreWiki.Application.Articles.Reading
{
	public interface IArticleReadingService
	{
		Task<ArticleReadingDto> GetArticleBySlug(string articleSlug);
		Task<bool> IsTopicAvailable(string articleSlug, int articleId);
		Task<SlugHistoryDto> GetSlugHistoryWithArticle(string slug);

		Task<IList<string>> GetArticlesToCreate(string slug);
		Task CreateComment(CreateCommentDto commentDto);

		Task<ArticleReadingDto> GetArticleById(int articleId);
		Task<ArticleReadingDto> GetArticleWithHistoriesBySlug(string articleSlug);

		Task<List<ArticleReadingDto>> GetLatestArticles(int numOfArticlesToGet);
	}
}
