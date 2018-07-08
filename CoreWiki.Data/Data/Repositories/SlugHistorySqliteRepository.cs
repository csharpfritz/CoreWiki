using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class SlugHistorySqliteRepository : ISlugHistoryRepository
	{
		public SlugHistorySqliteRepository(IApplicationDbContext context)
		{
			Context = context;
		}


		public IApplicationDbContext Context { get; }


		public async Task<SlugHistory> GetSlugHistoryWithArticle(string slug)
		{
			return await Context.SlugHistories.Include(h => h.Article)
				.OrderByDescending(h => h.Added)
				.FirstOrDefaultAsync(h => h.OldSlug == slug.ToLowerInvariant());
		}


		public void Dispose()
		{
			Context.Dispose();
		}
	}
}
