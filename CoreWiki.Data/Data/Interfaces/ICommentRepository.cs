using CoreWiki.Data.Models;
using System;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Interfaces
{
	public interface ICommentRepository : IDisposable
	{

		Task CreateComment(Comment commentModel);
	}
}
