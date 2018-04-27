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
        public DbSet<Comment> Comments { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
            .HasOne(p => p.Article)
            .WithMany(b => b.Comments)
            .HasForeignKey(p => p.IdArticle)
            .HasConstraintName("FK_Comment_Article");

            //Requirement Topic is unique
            modelBuilder.Entity<Article>()
            .HasIndex(b => b.Topic)
            .IsUnique()
            .HasFilter(null);

            //Requirement default value
            //modelBuilder.Entity<Comment>()
            //.Property(b => b.Submitted)
            //.HasDefaultValueSql("datetime()");

            //Performance
            //modelBuilder.Entity<Article>()
            //.HasIndex(b => b.Published)
            //.HasName("Index_Published");

            base.OnModelCreating(modelBuilder);
        }

    }
}
