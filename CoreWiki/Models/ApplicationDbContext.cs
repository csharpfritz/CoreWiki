using Microsoft.EntityFrameworkCore;
using NodaTime;
using System.Linq;

namespace CoreWiki.Models
{

	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Article>().HasIndex(a => a.Slug).IsUnique();

			modelBuilder.Entity<SlugHistory>().HasIndex(a => new { a.OldSlug, a.AddedDateTime });

		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<SlugHistory> SlugHistories { get; set; }

	internal static void SeedData(ApplicationDbContext context)
		{

			context.Database.Migrate();


			// Load an initial home page
			if (!context.Articles.Any(a => a.Topic == "HomePage"))
			{

				var homePageArticle = new Article
				{

					Topic = "HomePage",
					Slug= "home-page",
					Content = "This is the default home page.  Please change me!",
					Published = SystemClock.Instance.GetCurrentInstant()

				};
				context.Articles.Add(homePageArticle);
				context.SaveChanges();

			}


		}

	}
}
