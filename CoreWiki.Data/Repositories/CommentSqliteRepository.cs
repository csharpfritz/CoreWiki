using CoreWiki.Core.Interfaces;
using CoreWiki.Data.EntityFramework.Models;
using System.Threading.Tasks;

namespace CoreWiki.Data.EntityFramework.Repositories
{
	public class CommentSqliteRepository : ICommentRepository
	{

		public CommentSqliteRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }



		public async Task CreateComment(Core.Domain.Comment commentModel)
		{
			await Context.Comments.AddAsync(CommentDAO.FromDomain(commentModel));
			await Context.SaveChangesAsync();
		}


		public void Dispose()
		{
			this.Context.Dispose();
		}
	}
}
