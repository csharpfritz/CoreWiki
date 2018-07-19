using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureRouting(this IServiceCollection services)
		{
			services.AddRouting(options => options.LowercaseUrls = true);

			services.AddMvc(options =>
			{
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			})
				.AddViewLocalization()
				.AddDataAnnotationsLocalization()
				.AddRazorPagesOptions(options =>
				{
					options.Conventions.AddPageRoute("/Edit", "/{Slug}/Edit");
					options.Conventions.AddPageRoute("/Delete", "{Slug}/Delete");
					options.Conventions.AddPageRoute("/Details", "{Slug?}");
					options.Conventions.AddPageRoute("/Details", @"Index");
					options.Conventions.AddPageRoute("/Create", "{Slug?}/Create");
					options.Conventions.AddPageRoute("/History", "{Slug?}/History");
				});

			return services;
		}

		public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
		{
			app.UseStaticFiles();
			return app;
		}
	}
}
