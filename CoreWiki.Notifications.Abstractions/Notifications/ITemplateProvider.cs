using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CoreWiki.Notifications.Abstractions.Notifications
{
    public interface ITemplateProvider
    {
		IView GetTemplate(string templateName);
    }
}
