using CoreWiki.Core.Domain;
using System;
using System.Threading.Tasks;

namespace CoreWiki.Core.Interfaces
{
	public interface ICommentRepository : IDisposable
	{

		Task CreateComment(Comment commentModel);
	}
}
