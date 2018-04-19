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

		public DbSet<Article> Articles { get; set; }

		internal static void SeedData(ApplicationDbContext context)
		{

			context.Database.EnsureCreated();


			// Load an initial home page
			if (!context.Articles.Any(a => a.Topic == "HomePage"))
			{

				var homePageArticle = new Article
				{

					Topic = "HomePage",
					Content = "This is the default home page.  Please change me!",
					Published = SystemClock.Instance.GetCurrentInstant()

				};
				context.Articles.Add(homePageArticle);
				context.SaveChanges();

			}


		}

	}
}
