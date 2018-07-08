using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class CommentRepository : ICommentRepository
	{

		public CommentRepository(IApplicationDbContext context)
		{
			Context = (ApplicationDbContext)context;
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
