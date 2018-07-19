using CoreWiki.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace CoreWiki.Notifications
{
    public static class StartupExtensions
	{

		public static IServiceCollection AddEmailNotifications(this IServiceCollection services)
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
		    services.AddScoped<INotificationService, NotificationService>();

		    services.Configure<RazorViewEngineOptions>(options =>
		    {
                options.FileProviders.Add(
                    new EmbeddedFileProvider(typeof(TemplateProvider).GetTypeInfo().Assembly));
		    });
			return services;

		}


	}
}
