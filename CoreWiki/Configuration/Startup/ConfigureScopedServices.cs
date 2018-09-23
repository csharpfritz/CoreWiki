using System;
using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Impl;
using CoreWiki.Data.Abstractions.Interfaces;
using CoreWiki.Data.EntityFramework.Repositories;
using CoreWiki.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using WebEssentials.AspNetCore.Pwa;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureScopedServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IClock>(SystemClock.Instance);

			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddEmailNotifications(configuration);
			services.AddScoped<IArticlesSearchEngine, ArticlesDbSearchEngine>();

			services.AddTransient<IArticleRepository, ArticleRepositorySearchIndexingProxy>();
			services.AddScoped<ArticleRepository>();
			services.AddScoped<ArticleRepositorySearchIndexingProxy>();
			services.AddScoped<Func<int, IArticleRepository>>(serviceProvider => key =>
			{
				//TODO: enum
				switch (key)
				{
					case 1:
						return serviceProvider.GetService<ArticleRepository>();
					default:
						return serviceProvider.GetService<ArticleRepositorySearchIndexingProxy>();
				}
			});

			services.AddProgressiveWebApp(new PwaOptions { EnableCspNonce = true });
			return services;
		}
	}
}
