using CoreWiki.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Data.EntityFramework
{

	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var homePage = new ArticleDAO
			{
				Id = 1,
				Topic = "Home Page",
				Slug = "home-page",
				Content = "This is the default home page. Please change me!",
				Published = Instant.FromDateTimeUtc(new DateTime(2018, 6, 19, 14, 31, 2, 265, DateTimeKind.Utc)),
				AuthorId = Guid.Empty
			};

			var homePageHistory = ArticleHistoryDAO.FromArticle(homePage);
			homePageHistory.Id = 1;
			homePageHistory.Article = null;

			modelBuilder.Entity<ArticleDAO>(entity =>
			{
				entity.HasIndex(a => a.Slug).IsUnique();
				entity.HasData(homePage);
			});

			modelBuilder.Entity<ArticleHistoryDAO>(entity =>
			{
				entity.HasData(homePageHistory);
			});

			modelBuilder.Entity<SlugHistoryDAO>(entity =>
			{
				entity.HasIndex(a => new { a.OldSlug, a.AddedDateTime });
			});
		}

		public DbSet<ArticleDAO> Articles { get; set; }
		public DbSet<CommentDAO> Comments { get; set; }
		public DbSet<SlugHistoryDAO> SlugHistories { get; set; }

		public DbSet<ArticleHistoryDAO> ArticleHistories { get; set; }


		public override Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default(CancellationToken))
		{
			return base.SaveChangesAsync(cancellationToken);
		}


		public static void SeedData(ApplicationDbContext context)
		{

			context.Database.Migrate();

		}

	}
}
