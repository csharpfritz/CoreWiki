using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: HostingStartup(typeof(CoreWiki.Areas.Identity.IdentityHostingStartup))]
namespace CoreWiki.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
				bool.TryParse(context.Configuration["Authentication:RequireConfirmedEmail"],
					out var requireConfirmedEmail);

				ConfigureDb(context, services);

				services.AddIdentity<CoreWikiUser, IdentityRole>(options => {
					options.SignIn.RequireConfirmedEmail = requireConfirmedEmail;
					options.User.RequireUniqueEmail = true;
						})
					.AddRoles<IdentityRole>()
					.AddRoleManager<RoleManager<IdentityRole>>()
					.AddDefaultUI()
					.AddDefaultTokenProviders()
					.AddEntityFrameworkStores<CoreWikiIdentityContext>();

				var authBuilder = services.AddAuthentication();

				if (!string.IsNullOrEmpty(context.Configuration["Authentication:Microsoft:ApplicationId"]))
				{
					authBuilder.AddMicrosoftAccount(microsoftOptions =>
					{
						microsoftOptions.ClientId = context.Configuration["Authentication:Microsoft:ApplicationId"];
						microsoftOptions.ClientSecret = context.Configuration["Authentication:Microsoft:Password"];
					});
				}

				if (!string.IsNullOrEmpty(context.Configuration["Authentication:Twitter:ConsumerKey"]))
				{
					authBuilder.AddTwitter(twitterOptions =>
					{
						twitterOptions.ConsumerKey = context.Configuration["Authentication:Twitter:ConsumerKey"];
						twitterOptions.ConsumerSecret = context.Configuration["Authentication:Twitter:ConsumerSecret"];
					});
				}

				services.AddAuthorization(AuthPolicy.Execute);
			});
		}

		private static void ConfigureDb(WebHostBuilderContext context, IServiceCollection services)
		{

			Action<DbContextOptionsBuilder> optionsBuilder;
			var connectionString = context.Configuration.GetConnectionString("CoreWikiIdentityContextConnection");

			switch (context.Configuration["DataProvider"].ToLowerInvariant())
			{
				case "postgres":
					optionsBuilder = options => options.UseNpgsql(connectionString);
					break;
				default:
					connectionString = !string.IsNullOrEmpty(connectionString) ? connectionString : "DataSource =./App_Data/wikiIdentity.db";
					optionsBuilder = options => options.UseSqlite(connectionString);
					break;
			}

			services.AddDbContext<CoreWikiIdentityContext>(optionsBuilder);

		}
	}
}
