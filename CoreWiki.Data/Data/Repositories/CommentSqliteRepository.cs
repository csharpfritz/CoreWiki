using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class CommentSqliteRepository : ICommentRepository
	{

		public CommentSqliteRepository(IApplicationDbContext context)
		{
			Context = context;
		}

		public IApplicationDbContext Context { get; }



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
