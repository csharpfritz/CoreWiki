using CoreWiki.Core.Domain;
using System;
using System.Threading.Tasks;

namespace CoreWiki.Core.Interfaces
{
	public interface ISlugHistoryRepository : IDisposable
	{

		Task<SlugHistory> GetSlugHistoryWithArticle(string slug);

		Task AddToHistory(string oldSlug, Article article);

	}
}
