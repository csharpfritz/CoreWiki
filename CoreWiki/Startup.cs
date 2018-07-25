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

			services.AddSingleton<IExtensibilityManager, ExtensibilityManager>(); // MAC
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

			app.UseStatusCodePagesWithReExecute("/HttpErrors/{0}");
			app.UseMvc();
			// MAC - Extensibility API
			ModuleEvents = new CoreWikiModuleEvents();
			var modulesConfig = Configuration.Get<AppSettings>().ExtensibilityModules;
			foreach (var moduleConfig in modulesConfig)
			{
				var module = Activator.CreateInstance(Type.GetType(moduleConfig.Type)) as ICoreWikiModule;
				if (module != null)
				{
					module.Initialize(ModuleEvents);
				}
			}
		}

		static public CoreWikiModuleEvents ModuleEvents { get; set; } // MAC
	}
}
