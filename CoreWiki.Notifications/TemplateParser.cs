using CoreWiki.Core.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreWiki.Notifications
{
    public class TemplateParser : ITemplateParser
	{
	    private readonly IServiceProvider _serviceProvider;
	    private readonly ITempDataProvider _tempDataProvider;

	    public TemplateParser(IServiceProvider serviceProvider, ITempDataProvider tempDataProvider)
	    {
	        _serviceProvider = serviceProvider;
	        _tempDataProvider = tempDataProvider;
	    }

	    public async Task<string> Parse<TModel>(IView view, TModel model) where TModel : class
	    {
	        using (var output = new StringWriter())
	        {
	            var actionContext = GetActionContext();
	            var tempDataDictionary = GetTempDataDictionary(actionContext);
	            var viewDataDictionary = GetViewDictionary(model);

	            var viewContext = new ViewContext(
	                actionContext,
	                view,
	                viewDataDictionary,
	                tempDataDictionary,
	                output,
	                new HtmlHelperOptions());

	            await view.RenderAsync(viewContext);

	            return output.ToString();
	        }
	    }

	    private TempDataDictionary GetTempDataDictionary(ActionContext actionContext)
	    {
	        return new TempDataDictionary(
	            actionContext.HttpContext,
	            _tempDataProvider);
	    }

	    private ViewDataDictionary GetViewDictionary<TModel>(TModel model) where TModel : class
	    {
	        return new ViewDataDictionary(
	            new EmptyModelMetadataProvider(),
	            new ModelStateDictionary())
	        {
	            Model = model
	        };
	    }

	    private ActionContext GetActionContext()
	    {
	        var httpContext = new DefaultHttpContext
	        {
	            RequestServices = _serviceProvider
	        };
	        return new ActionContext(
	            httpContext,
	            new RouteData(),
	            new ActionDescriptor());
	    }
    }
}
