using System;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;

namespace CoreWiki.Data.Abstractions.Interfaces
{
	public interface ISlugHistoryRepository : IDisposable
	{

		Task<SlugHistory> GetSlugHistoryWithArticle(string slug);

		Task AddToHistory(string oldSlug, Article article);

		Task DeleteAllHistoryOfArticle(int articleId);
	}
}
