using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreWiki.Models
{
	public interface IApplicationDbContext
	{
		DbSet<Article> Articles { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<SlugHistory> SlugHistories { get; set; }

		Task SaveChangesAsync();


	}
}
