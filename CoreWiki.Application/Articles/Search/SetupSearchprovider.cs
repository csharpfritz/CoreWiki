using CoreWiki.Application.Articles.Search.AzureSearch;
using CoreWiki.Application.Articles.Search.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Application.Articles.Search
{
	public static class SetupSearchprovider
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
