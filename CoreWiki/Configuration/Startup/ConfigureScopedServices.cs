using CoreWiki.Core.Notifications;
using CoreWiki.Notifications;
using CoreWiki.SearchEngines;
using CoreWiki.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using WebEssentials.AspNetCore.Pwa;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
		{
			services.AddSingleton<IClock>(SystemClock.Instance);

			services.AddHttpContextAccessor();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddScoped<IArticlesSearchEngine, ArticlesDbSearchEngine>();
			services.AddScoped<ITemplateProvider, TemplateProvider>();
			services.AddScoped<ITemplateParser, TemplateParser>();
			services.AddScoped<IEmailMessageFormatter, EmailMessageFormatter>();
			services.AddScoped<IEmailNotifier, EmailNotifier>();
			services.AddScoped<INotificationService, NotificationService>();

			services.AddProgressiveWebApp(new PwaOptions { EnableCspNonce = true });

			return services;
		}
	}
}
