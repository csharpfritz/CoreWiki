using Microsoft.Azure.Search;

namespace CoreWiki.Application.Articles.Search.AzureSearch
{
	public interface IAzureSearchClient
	{
		/// <summary>
		/// This client can be used for search
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ISearchIndexClient GetSearchClient<T>();

		/// <summary>
		/// This client can be used to Index elements
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ISearchIndexClient CreateServiceClient<T>();
	}
}
