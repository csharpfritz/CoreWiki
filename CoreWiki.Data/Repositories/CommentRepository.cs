using System.Linq;
using CoreWiki.Data.EntityFramework.Models;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;

namespace CoreWiki.Data.EntityFramework.Repositories
{
	public class CommentRepository : ICommentRepository
	{

		public CommentRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }

		public async Task CreateComment(Comment comment)
		{
			await Context.Comments.AddAsync(CommentDAO.FromDomain(comment));
			await Context.SaveChangesAsync();
		}

		public async Task DeleteAllCommentsOfArticle(int articleId)
		{
			Context.Comments.RemoveRange(Context.Comments.Where(c => c.IdArticle == articleId));
			await Context.SaveChangesAsync();
		}


		public void Dispose()
		{
			this.Context.Dispose();
		}

	}
}
