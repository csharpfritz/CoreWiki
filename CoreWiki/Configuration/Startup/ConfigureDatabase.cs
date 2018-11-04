using CoreWiki.Data.EntityFramework;
using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
		{

			// Exit now if we don't have a data configuration
			if (string.IsNullOrEmpty(config["DataProvider"])) return services;

			services.AddRepositories(config);
			return services;
		}

		/// <summary>
		/// Initialize the database with appropriate schema and content 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IApplicationBuilder InitializeData(this IApplicationBuilder app, IConfiguration config)
		{

			// Exit now if we don't have a data configuration
			if (string.IsNullOrEmpty(config["DataProvider"])) return app;

			var scope = app.ApplicationServices.CreateScope();

			var identityContext = scope.SeedData()
				.ServiceProvider.GetService<CoreWikiIdentityContext>();
			CoreWikiIdentityContext.SeedData(identityContext);

			return app;
		}
	}
}
