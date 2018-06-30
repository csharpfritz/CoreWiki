using System;
using System.Threading.Tasks;
using CoreWiki.Models;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace CoreWiki.Test
{

	public class InMemoryDbContext : DbContext, IApplicationDbContext
	{

		public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Article>().HasIndex(a => a.Slug).IsUnique();

			modelBuilder.Entity<Article>().HasData(new[] {
				new Article
					{
						Id=1,
						Topic = "HomePage",
						Slug= "home-page",
						Content = "This is the default home page.  Please change me!",
						Published = SystemClock.Instance.GetCurrentInstant(),
						AuthorId = Guid.NewGuid()
					}
			});

			modelBuilder.Entity<SlugHistory>().HasIndex(a => new { a.OldSlug, a.AddedDateTime });

		}

		public virtual DbSet<Article> Articles { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<SlugHistory> SlugHistories { get; set; }


		async Task IApplicationDbContext.SaveChangesAsync()
		{
			await SaveChangesAsync();
		}

		public static void SeedData(InMemoryDbContext context)
		{

			context.Database.EnsureCreated();

		}

	}
}
