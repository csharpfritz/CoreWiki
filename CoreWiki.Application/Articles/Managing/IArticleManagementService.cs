using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Managing
{
	public interface IArticleManagementService
	{
		Task<Article> CreateArticleAndHistory(Article article);
		Task<Article> Update(int id, string topic, string content, Guid authorId, string authorName);
		Task<Article> Delete(string slug);
		Task<bool> IsTopicAvailable(string articleSlug, int articleId);
		Task<ArticleManageDto> GetArticleBySlug(string articleSlug);

		Task<IList<string>> GetArticlesToCreate(string slug);
		Task<(string,IList<string>)> GetArticlesToCreate(int articleId);
	}
}
