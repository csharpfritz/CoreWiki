using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public interface ITemplateProvider
    {
		Task<string> GetTemplateContent(string templateName);
    }
}
