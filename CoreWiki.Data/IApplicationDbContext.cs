using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreWiki.Data
{
	public interface IApplicationDbContext : IDisposable
	{

		DbSet<Article> Articles { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<SlugHistory> SlugHistories { get; set; }

		DbSet<ArticleHistory> ArticleHistories { get; set; }

	}
}
