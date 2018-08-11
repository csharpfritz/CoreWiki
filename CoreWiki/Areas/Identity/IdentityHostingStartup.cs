using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

				services.AddDbContext<CoreWikiIdentityContext>(options =>
					options.UseSqlite(
						context.Configuration.GetConnectionString("CoreWikiIdentityContextConnection")));

				services.AddIdentity<CoreWikiUser, IdentityRole>(options =>
						options.SignIn.RequireConfirmedEmail = requireConfirmedEmail)
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
	}
}
