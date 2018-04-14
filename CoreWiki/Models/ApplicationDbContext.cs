using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Models
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<Article> Articles { get; set; }

	
	}
}
