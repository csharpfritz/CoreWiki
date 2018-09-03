using System;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Services
{
	public interface IArticleManagementService
	{
		Task<Article> CreateArticleAndHistory(Article article);
		Task Update(int id, string topic, string content, Guid authorId, string authorName);
		Task<Article> Delete(string slug);
		//Task AddToHistory(string oldSlug, Article article);
	}
}
