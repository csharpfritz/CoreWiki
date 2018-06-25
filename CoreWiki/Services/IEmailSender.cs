using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface IEmailSender
    {
		Task<bool> SendEmailAsync(string recipientEmail, string subject, string body);
		Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body);

	}
}
