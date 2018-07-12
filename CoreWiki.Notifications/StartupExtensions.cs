using CoreWiki.Core.Configuration;
using CoreWiki.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;

namespace CoreWiki.Notifications
{
	public static class StartupExtensions
	{

		public static IServiceCollection AddEmailNotifications(this IServiceCollection services) {

			services.AddScoped<ITemplateProvider, TemplateProvider>();
			services.AddScoped<ITemplateParser, TemplateParser>();
			services.AddScoped<IEmailMessageFormatter, EmailMessageFormatter>();
			services.AddScoped<IEmailNotifier, EmailNotifier>();

		    services.Configure<RazorViewEngineOptions>(options =>
		    {
                options.FileProviders.Add(
                    new EmbeddedFileProvider(typeof(TemplateProvider).GetTypeInfo().Assembly));
		    });
			return services;

		}


	}
}
