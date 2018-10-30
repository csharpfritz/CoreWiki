using CoreWiki.Areas.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
					options.Conventions.AddPageRoute("/Details", "/");
					// options.Conventions.AddPageRoute("/Details", "/wiki/{Slug?}");
					options.Conventions.AddPageRoute("/Details", @"Index");
					options.Conventions.AddPageRoute("/Create", "{Slug?}/Create");
					options.Conventions.AddPageRoute("/History", "{Slug?}/History");
					options.Conventions.AuthorizeAreaFolder("Identity", "/UserAdmin", PolicyConstants.CanManageRoles);
					options.Conventions.AddPageRoute("/Details", "{Slug?}");
				})
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			return services;
		}

		public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
		{
			app.UseStaticFiles();
			return app;
		}
	}
}
