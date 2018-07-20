using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureLocalisation(this IServiceCollection services)
		{
			services.AddLocalization(options => options.ResourcesPath = "Globalization");
			return services;
		}

		public static IApplicationBuilder ConfigureLocalisation(this IApplicationBuilder app)
		{
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
			});

			return app;
		}
	}
}
