using System.Threading.Tasks;

namespace CoreWiki.Core.Notifications
{
	public interface ITemplateProvider
    {
		Task<string> GetTemplateContent(string templateName);
    }
}
