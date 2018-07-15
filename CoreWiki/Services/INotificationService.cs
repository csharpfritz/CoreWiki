using CoreWiki.Data.Models;
using CoreWiki.Data.Security;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface INotificationService
	{
		Task<bool> NotifyAuthorNewComment(CoreWikiUser author, Article article, Comment comment);
	}
}
