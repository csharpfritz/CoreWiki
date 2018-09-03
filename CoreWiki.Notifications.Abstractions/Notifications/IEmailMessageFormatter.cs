using System.Threading.Tasks;

namespace CoreWiki.Notifications.Abstractions.Notifications
{
	public interface IEmailMessageFormatter
    {
		Task<string> FormatEmailMessage<T>(string templateName, T model) where T : class;
    }
}
