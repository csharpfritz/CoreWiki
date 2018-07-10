using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Data
{
	public interface IApplicationDbContext : IDisposable
	{

		DbSet<Article> Articles { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<SlugHistory> SlugHistories { get; set; }

		DbSet<ArticleHistory> ArticleHistories { get; set; }


		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

	}
}
