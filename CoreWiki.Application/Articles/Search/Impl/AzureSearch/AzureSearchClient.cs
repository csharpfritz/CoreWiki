using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CoreWiki.Application.Articles.Search.AzureSearch
{
	internal class AzureSearchClient : IAzureSearchClient
	{
		private readonly IOptionsSnapshot<SearchProviderSettings> _config;
		private readonly IConfiguration _configuration;

		private string searchServiceName => _config.Value.Az_ApiGateway; //_configuration["SearchProvider:Az_ApiGateway"];
		private string adminApiKey => _config.Value.Az_ReadApiKey; //_configuration["SearchProvider:Az_ReadApiKey"];
		private string queryApiKey => _config.Value.Az_WriteApiKey; //_configuration["SearchProvider:Az_WriteApiKey"];

		public AzureSearchClient(IOptionsSnapshot<SearchProviderSettings> config, IConfiguration configuration)
		{
			_config = config;
			_configuration = configuration;
		}

		public ISearchIndexClient CreateServiceClient<T>()
		{
			var index = typeof(T).FullName;
			var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
			return GetOrCreateIndex<T>(serviceClient);
		}

		private ISearchIndexClient GetOrCreateIndex<T>(SearchServiceClient serviceClient)
		{
			var index = typeof(T).FullName;
			if (serviceClient.Indexes.Exists(index))
			{
				return serviceClient.Indexes.GetClient(index);
			}

			var definition = new Index()
			{
				Name = index,
				Fields = FieldBuilder.BuildForType<T>()
			};

			serviceClient.Indexes.Create(definition);

			return serviceClient.Indexes.GetClient(index);
		}

		public ISearchIndexClient GetSearchClient<T>()
		{
			var indexClient = new SearchIndexClient(searchServiceName, typeof(T).FullName, new SearchCredentials(queryApiKey));
			return indexClient;
		}
	}
}
