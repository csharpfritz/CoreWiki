using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CoreWiki.FirstStart
{

	public static class StartupExtensions
	{
		private static bool _RunAfterConfiguration = false;
		private static bool _FirstStartIncomplete = true;
		private static string _AppConfigurationFilename;
		public static Func<Task> _RestartHost;
		private static IConfiguration Configuration;

		public static IServiceCollection AddFirstStartConfiguration(this IServiceCollection services, IConfiguration configuration)
		{

			// services.AddSingleton<FirstStartConfiguration>(new FirstStartConfiguration());

			Configuration = configuration;

			return services;

		}
		public static IApplicationBuilder UseFirstStartConfiguration(this IApplicationBuilder app, IHostingEnvironment hostingEnvironment, IConfiguration configuration, Func<Task> restartHost)
		{

			_AppConfigurationFilename = Path.Combine(hostingEnvironment.ContentRootPath, "appsettings.json");
			_RestartHost = restartHost;

			app.UseWhen(IsFirstStartIncomplete, thisApp =>
			{

				thisApp.MapWhen(context => !context.Request.Path.StartsWithSegments("/FirstStart"), mapApp =>
					mapApp.Run(request =>
					{
						request.Response.Redirect("/FirstStart");
						return Task.CompletedTask;
					})

					);

				thisApp.UseMvc();

			});

			return app;

		}

		private static bool IsFirstStartIncomplete(HttpContext context)
		{

			return string.IsNullOrEmpty(Configuration["DataProvider"]);

		}

	}

}
