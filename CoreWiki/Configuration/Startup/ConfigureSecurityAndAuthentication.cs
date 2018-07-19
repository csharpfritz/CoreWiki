using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureSecurityAndAuthentication(this IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			return services;
		}

		public static IApplicationBuilder ConfigureSecurityHeaders(this IApplicationBuilder app)
		{
			app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());
			app.UseXContentTypeOptions();
			app.UseReferrerPolicy(options => options.NoReferrer());
			app.UseXXssProtection(options => options.EnabledWithBlockMode());
			app.UseXfo(options => options.Deny());
			app.UseHttpsRedirection();
			return app;
		}

		public static IApplicationBuilder ConfigureAuthentication(this IApplicationBuilder app)
		{
			app.UseCookiePolicy();
			app.UseAuthentication();
			return app;
		}
	}
}
