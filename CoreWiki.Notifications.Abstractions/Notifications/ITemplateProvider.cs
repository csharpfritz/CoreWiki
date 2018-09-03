using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CoreWiki.Core.Notifications
{
    public interface ITemplateProvider
    {
		IView GetTemplate(string templateName);
    }
}
