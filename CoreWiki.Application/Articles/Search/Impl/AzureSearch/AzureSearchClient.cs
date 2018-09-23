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

		private string SearchServiceName => _config.Value.Az_ApiGateway;
		private string AdminApiKey => _config.Value.Az_WriteApiKey;
		private string QueryApiKey => _config.Value.Az_ReadApiKey;
		private string GetIndexName<T>()
		{
			return typeof(T).Name.ToLowerInvariant();
		}

		public AzureSearchClient(IOptionsSnapshot<SearchProviderSettings> config, IConfiguration configuration)
		{
			_config = config;
			_configuration = configuration;
		}

		public ISearchIndexClient CreateServiceClient<T>()
		{
			var index = typeof(T).FullName;
			var serviceClient = new SearchServiceClient(SearchServiceName, new SearchCredentials(AdminApiKey));
			return GetOrCreateIndex<T>(serviceClient);
		}

		private ISearchIndexClient GetOrCreateIndex<T>(SearchServiceClient serviceClient)
		{
			//indexname must be lowercase
			var index = GetIndexName<T>();
			if (serviceClient.Indexes.Exists(index))
			{
				return serviceClient.Indexes.GetClient(index);
			}


			try
			{
				var definition = new Index()
				{
					Name = index,
					// NodaTimecrash
					Fields = FieldBuilder.BuildForType<T>()
				};
				var createdindex = serviceClient.Indexes.Create(definition);
			}
			catch (System.Exception e)
			{
				throw;
			}

			return serviceClient.Indexes.GetClient(index);
		}

		public ISearchIndexClient GetSearchClient<T>()
		{
			var indexClient = new SearchIndexClient(SearchServiceName, GetIndexName<T>(), new SearchCredentials(QueryApiKey));
			return indexClient;
		}


	}
}
