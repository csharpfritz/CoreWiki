using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreWiki.FirstStart
{

	public static class StartupExtensions
	{

		public static IServiceCollection UseFirstStartConfiguration(this IServiceCollection services) {

			services.AddSingleton<FirstStartConfiguration>(new FirstStartConfiguration());

			return services;

		}

		public static IApplicationBuilder UseFirstStartConfiguration(this IApplicationBuilder app, IConfiguration configuration) {

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
			// NOTE: Forcing this to true while building the functionality
			// in the project_firstStart branch
			return false;
		}

		public static IServiceCollection AddFirstStartConfiguration(this IServiceCollection services) {

			return services;

		}

	}

}
