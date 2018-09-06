using System;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;

namespace CoreWiki.Data.Abstractions.Interfaces
{
	public interface ICommentRepository : IDisposable
	{
		Task CreateComment(Comment comment);
		Task DeleteAllCommentsOfArticle(int articleId);
	}
}
