using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Impl;
using CoreWiki.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using WebEssentials.AspNetCore.Pwa;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureScopedServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IClock>(SystemClock.Instance);

			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddEmailNotifications(configuration);
			services.AddScoped<IArticlesSearchEngine, ArticlesDbSearchEngine>();

			services.AddProgressiveWebApp(new PwaOptions {
				EnableCspNonce = true,
				RegisterServiceWorker = false
			});

			return services;
		}
	}
}
