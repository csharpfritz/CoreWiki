using System.Threading.Tasks;

namespace CoreWiki.Core.Notifications
{
	public interface INotificationService
	{
		Task<bool> SendConfirmationEmail(string confirmEmail, string userId, string confirmCode);
		Task<bool> SendForgotPasswordEmail(string accountEmail, string resetToken);
		Task<bool> SendNewCommentEmail(string authorEmail, string authorName, string commenterName, string topic, string articleSlug);
	}
}
