using System.Threading.Tasks;

namespace CoreWiki.Core.Notifications
{
	public interface IEmailMessageFormatter
    {
		Task<string> FormatEmailMessage<T>(string templateName, T model) where T : class;
    }
}
