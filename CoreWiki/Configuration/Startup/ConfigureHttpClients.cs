using CoreWiki.Areas.Identity.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
		{
			services.AddHttpClient<HIBPClient>();
			return services;
		}
	}
}
