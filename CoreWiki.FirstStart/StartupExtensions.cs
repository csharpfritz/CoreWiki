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

		private static bool _FirstStartIncomplete = true;
        private static string _AppConfigurationFilename;


        public static IServiceCollection AddFirstStartConfiguration(this IServiceCollection services) {

			// services.AddSingleton<FirstStartConfiguration>(new FirstStartConfiguration());

			return services;

		}
		public static IApplicationBuilder UseFirstStartConfiguration(this IApplicationBuilder app, IHostingEnvironment hostingEnvironment, IConfiguration configuration) {

			_AppConfigurationFilename =	Path.Combine(hostingEnvironment.ContentRootPath, "appsettings.app.json");

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

			return false;

			if (_FirstStartIncomplete && !File.Exists(_AppConfigurationFilename)) {
				return _FirstStartIncomplete;
			}

			_FirstStartIncomplete = false;
			return false;

		}

	}

}
