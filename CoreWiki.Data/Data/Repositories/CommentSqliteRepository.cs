using CoreWiki.Core.Interfaces;
using CoreWiki.Data.Models;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class CommentSqliteRepository : ICommentRepository
	{

		public CommentSqliteRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }



		public async Task CreateComment(Comment commentModel)
		{
			await Context.Comments.AddAsync(commentModel);
			await Context.SaveChangesAsync();
		}


		public void Dispose()
		{
			this.Context.Dispose();
		}
	}
}
