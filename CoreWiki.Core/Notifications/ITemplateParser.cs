using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CoreWiki.Core.Notifications
{
	public interface ITemplateParser
	{
	    Task<string> Parse<TModel>(IView view, TModel model) where TModel : class;
	}
}
