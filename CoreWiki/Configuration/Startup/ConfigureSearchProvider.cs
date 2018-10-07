using System;
using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Impl;
using CoreWiki.Azure.Areas.AzureSearch;
using CoreWiki.Data.Abstractions.Interfaces;
using CoreWiki.Data.EntityFramework.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureSearchProvider(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IArticlesSearchEngine, ArticlesDbSearchEngine>();

			//Testing scrutor (https://github.com/khellang/Scrutor): getting overflowexception
			//services.AddScoped<IArticleRepository, ArticleRepository>();
			//services.Decorate<IArticleRepository, ArticleRepositorySearchIndexingProxy>();

			services.AddScoped<ArticleRepository>();
			services.AddScoped<IArticleRepository, ArticleRepositorySearchIndexingProxy>();
			services.AddScoped<Func<int, IArticleRepository>>(serviceProvider => key =>
			{
				//TODO: enum or other DI that supports decorator
				switch (key)
				{
					default:
						return serviceProvider.GetService<ArticleRepository>();
				}
			});

			switch (configuration["SearchProvider"])
			{
				case "Az":
					services.AddTransient(typeof(ISearchProvider<>), typeof(AzureSearchProvider<>));
					services.AddTransient<IAzureSearchClient, AzureSearchClient>();
					break;
				default:
					services.AddTransient(typeof(ISearchProvider<>), typeof(LocalDbArticleSearchProviderAdapter<>));
					break;
			}
			return services;
		}
	}
}
