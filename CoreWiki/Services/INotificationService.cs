using CoreWiki.Models;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface INotificationService
    {
		Task<bool> NotifyAuthorNewComment(Article article, Comment comment);

	}
}
