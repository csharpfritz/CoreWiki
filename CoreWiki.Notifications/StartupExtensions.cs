using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using CoreWiki.Notifications.Abstractions.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using CoreWiki.Notifications.Abstractions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreWiki.Notifications
{
	public static class StartupExtensions
	{

		public static IServiceCollection AddEmailNotifications(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHttpContextAccessor();

			services.AddScoped<INotificationService, NotificationService>();
			services.AddScoped<IUrlHelper>(x =>
			{
				var actionContext = x.GetService<IActionContextAccessor>().ActionContext;
				return new UrlHelper(actionContext);
			});

			services.AddScoped<ITemplateProvider, TemplateProvider>();
			services.AddScoped<ITemplateParser, TemplateParser>();
			services.AddScoped<IEmailMessageFormatter, EmailMessageFormatter>();
			services.AddScoped<IEmailNotifier, EmailNotifier>();
			services.AddScoped<INotificationService, NotificationService>(sp =>
				{
					var emailMessageFormatter = sp.GetService<IEmailMessageFormatter>();
					var emailNotifier = sp.GetService<IEmailNotifier>();
					var settings = sp.GetService<IOptionsSnapshot<EmailNotifications>>();
					var loggerFactory = sp.GetService<ILoggerFactory>();
					return new NotificationService(emailMessageFormatter,
						emailNotifier,
						settings,
						loggerFactory,
						configuration.GetSection("url").Value );
				}
			);

			services.Configure<RazorViewEngineOptions>(options =>
			{
				options.FileProviders.Add(
					new EmbeddedFileProvider(typeof(TemplateProvider).GetTypeInfo().Assembly));
			});
			return services;

		}


	}
}
