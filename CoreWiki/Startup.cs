using CoreWiki.Configuration;
using CoreWiki.Configuration.Startup;
using CoreWiki.Core.Configuration;
using CoreWiki.Extensibility.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using CoreWiki.Notifications;

namespace CoreWiki
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureRSSFeed();
			services.Configure<AppSettings>(Configuration);
			services.ConfigureSecurityAndAuthentication();
			services.ConfigureDatabase(Configuration);
			services.ConfigureScopedServices();
			services.ConfigureRouting();
			services.ConfigureLocalisation();
			services.ConfigureExtensibility();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionsSnapshot<AppSettings> settings)
		{
			app.ConfigureTelemetry();
			app.ConfigureExceptions(env);
			app.ConfigureSecurityHeaders();
			app.ConfigureRouting();
			app.ConfigureAuthentication();
			app.ConfigureRSSFeed(settings);
			app.ConfigureLocalisation();
			app.ConfigureDatabase();

			app.UseExtensibility();
			app.UseStatusCodePagesWithReExecute("/HttpErrors/{0}");
			app.UseMvc();
        }
	}
}
