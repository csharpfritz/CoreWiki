using System;
using CoreWiki.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Snickler.RSSCore.Extensions;
using Snickler.RSSCore.Models;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IApplicationBuilder ConfigureRSSFeed(this IApplicationBuilder app, IOptionsSnapshot<AppSettings> settings)
		{
			app.UseRSSFeed("/feed", new RSSFeedOptions
			{
				Title = "CoreWiki RSS Feed",
				Copyright = DateTime.UtcNow.Year.ToString(),
				Description = "RSS Feed for CoreWiki",
				Url = settings.Value.Url
			});

			return app;
		}

		public static IServiceCollection ConfigureRSSFeed(this IServiceCollection services)
		{
			services.AddRSSFeed<RSSProvider>();
			return services;
		}
	}
}
