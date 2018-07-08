using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;

namespace CoreWiki.Data
{

	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var homePage = new Article
			{
				Id = 1,
				Topic = "HomePage",
				Slug = "home-page",
				Content = "This is the default home page.  Please change me!",
				Published = SystemClock.Instance.GetCurrentInstant(),
				AuthorId = Guid.NewGuid()
			};

			var homePageHistory = ArticleHistory.FromArticle(homePage);
			homePageHistory.Id = 1;
			homePageHistory.Article = null;

			modelBuilder.Entity<Article>(entity =>
			{
				entity.HasIndex(a => a.Slug).IsUnique();
				entity.HasData(homePage);
			});

			modelBuilder.Entity<ArticleHistory>(entity =>
			{
				entity.HasData(homePageHistory);
			});

			modelBuilder.Entity<SlugHistory>(entity =>
			{
				entity.HasIndex(a => new { a.OldSlug, a.AddedDateTime });
			});
		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<SlugHistory> SlugHistories { get; set; }

		public DbSet<ArticleHistory> ArticleHistories { get; set; }

		public static void SeedData(ApplicationDbContext context)
		{

			//context.Database.Migrate();

		}

	}
}
