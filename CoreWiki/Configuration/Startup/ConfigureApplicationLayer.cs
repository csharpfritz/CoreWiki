using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing;
using CoreWiki.Application.Articles.Managing.Impl;
using CoreWiki.Application.Articles.Reading;
using CoreWiki.Application.Articles.Reading.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static class ConfigureApplicationLayer
	{
		public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
		{
			services.AddTransient<IArticleReadingService, ArticleReadingService>();
			services.AddTransient<IArticleManagementService, ArticleManagementService>();
			return services;
		}
	}
}
