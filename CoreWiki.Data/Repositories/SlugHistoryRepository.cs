using CoreWiki.Data.Abstractions.Interfaces;
using CoreWiki.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Data.EntityFramework.Repositories
{
	public class SlugHistoryRepository : ISlugHistoryRepository
	{
		public SlugHistoryRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }

		public async Task<Core.Domain.SlugHistory> GetSlugHistoryWithArticle(string slug)
		{
			var res = await Context.SlugHistories.Include(h => h.Article)
				.OrderByDescending(h => h.Added)
				.FirstOrDefaultAsync(h => h.OldSlug == slug.ToLowerInvariant())
				.ConfigureAwait(false);
			return res?.ToDomain();
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

		public async Task DeleteAllHistoryOfArticle(int articleId)
		{
			Context.SlugHistories.RemoveRange(Context.SlugHistories.Where(sh => sh.Article.Id == articleId));
			await Context.SaveChangesAsync();
		}
	}
}
