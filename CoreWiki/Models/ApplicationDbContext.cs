using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Linq;

namespace CoreWiki.Models
{

	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Article>().HasIndex(a => a.Slug).IsUnique();

			var homePage = new Article
					{
						Id=1,
						Topic = "HomePage",
						Slug= "home-page",
						Content = "This is the default home page.  Please change me!",
						Published = SystemClock.Instance.GetCurrentInstant(),
						AuthorId = Guid.NewGuid()
					};
			var homePageHistory = ArticleHistory.FromArticle(homePage);
			modelBuilder.Entity<Article>().HasData(new[] {
				homePage
			});
			modelBuilder.Entity<ArticleHistory>().HasData(new[] {
				homePageHistory
			});

			modelBuilder.Entity<SlugHistory>().HasIndex(a => new { a.OldSlug, a.AddedDateTime });

		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<SlugHistory> SlugHistories { get; set; }

		public DbSet<ArticleHistory> ArticleHistories { get; set; }

	internal static void SeedData(ApplicationDbContext context)
		{

			context.Database.Migrate();

		}

	}
}
