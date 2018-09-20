using System;
using System.Threading.Tasks;

namespace CoreWiki.Notifications.Abstractions.Notifications
{
	public interface INotificationService
	{
		Task<bool> SendConfirmationEmail(string confirmEmail, string userId, string confirmCode);
		Task<bool> SendForgotPasswordEmail(string accountEmail, string resetToken, Func<bool> canNotifyUser);
		Task<bool> SendNewCommentEmail(string authorEmail, string authorName, string commenterName, string topic, string articleSlug, Func<bool> canNotifyUser);
	}
}
