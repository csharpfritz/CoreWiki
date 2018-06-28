using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface IEmailMessageFormatter
    {
		Task<string> FormatEmailMessage<T>(string templateName, T model) where T : class;
    }
}
