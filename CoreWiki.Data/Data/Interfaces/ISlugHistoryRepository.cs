using CoreWiki.Data.Models;
using System;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Interfaces
{
	public interface ISlugHistoryRepository : IDisposable
	{

		Task<SlugHistory> GetSlugHistoryWithArticle(string slug);

		Task AddToHistory(string oldSlug, Article article);

	}
}
