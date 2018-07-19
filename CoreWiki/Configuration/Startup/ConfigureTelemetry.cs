using CoreWiki.Helpers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IApplicationBuilder ConfigureTelemetry(this IApplicationBuilder app)
		{
			var initializer = new ArticleNotFoundInitializer();

			var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
			configuration.TelemetryInitializers.Add(initializer);

			return app;
		}
	}
}
