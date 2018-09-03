using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace CoreWiki.Test.Website.Pages
{

/// <summary>
/// A collection of useful methods for testing our PageModels
/// </summary>
	public static class PageModelExtensions
	{

		public static void AddPageContext(this PageModel pageModel, string userName, Guid userId, IEnumerable<Claim> additionalClaims = null)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, userName),
				new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
				new Claim("name", userName)
			};

			claims.AddRange(additionalClaims ?? new Claim[] { });

			var identity = new ClaimsIdentity(claims);
			var principle = new ClaimsPrincipal(identity);
			// use default context with user
			var httpContext = new DefaultHttpContext()
			{
				User = principle
			};
			//need these as well for the page context
			var modelState = new ModelStateDictionary();
			var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
			var modelMetadataProvider = new EmptyModelMetadataProvider();
			var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
			// need page context for the page model

			pageModel.PageContext = new PageContext(actionContext)
			{
				ViewData = viewData
			};

		}



	}
}
