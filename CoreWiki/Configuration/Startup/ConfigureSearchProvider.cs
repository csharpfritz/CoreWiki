using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Impl;
using CoreWiki.Azure.Areas.AzureSearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureSearchProvider(this IServiceCollection services, IConfiguration configuration)
		{
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
