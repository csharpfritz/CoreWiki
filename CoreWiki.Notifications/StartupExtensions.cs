using CoreWiki.Core.Configuration;
using CoreWiki.Core.Notifications;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Notifications
{
	public static class StartupExtensions
	{

		public static IServiceCollection AddEmailNotifications(this IServiceCollection services) {

			services.AddScoped<ITemplateProvider, TemplateProvider>();
			services.AddScoped<ITemplateParser, TemplateParser>();
			services.AddScoped<IEmailMessageFormatter, EmailMessageFormatter>();
			services.AddScoped<IEmailNotifier, EmailNotifier>();

			return services;

		}


	}
}
