using CoreWiki.Core.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class SlugHistorySqliteRepository : ISlugHistoryRepository
	{
		public SlugHistorySqliteRepository(ApplicationDbContext context)
		{
			Context = context;
		}


		public ApplicationDbContext Context { get; }


		public async Task<Core.Domain.SlugHistory> GetSlugHistoryWithArticle(string slug)
		{
			return (await Context.SlugHistories.Include(h => h.Article)
				.OrderByDescending(h => h.Added)
				.FirstOrDefaultAsync(h => h.OldSlug == slug.ToLowerInvariant()))
				.ToDomain();
		}


		public void Dispose()
		{
			Context.Dispose();
		}

		public Task AddToHistory(string oldSlug, Core.Domain.Article article)
		{

			var newSlug = new SlugHistoryDAO()
			{
				OldSlug = oldSlug,
				Article = ArticleDAO.FromDomain(article),
				AddedDateTime = DateTime.UtcNow
			};

			Context.SlugHistories.Add(newSlug);
			return Context.SaveChangesAsync();

		}
	}
}
