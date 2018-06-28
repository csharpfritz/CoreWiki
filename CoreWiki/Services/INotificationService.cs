using CoreWiki.Areas.Identity.Data;
using CoreWiki.Models;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface INotificationService
    {
		Task<bool> NotifyAuthorNewComment(CoreWikiUser author, Article article, Comment comment);
	}
}
