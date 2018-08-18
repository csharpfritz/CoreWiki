using System.Threading.Tasks;
using CoreWiki.Configuration.Startup;
using CoreWiki.Core.Configuration;
using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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

			services.ConfigureAutomapper();

			services.ConfigureRSSFeed();
			services.Configure<AppSettings>(Configuration);
			services.ConfigureSecurityAndAuthentication();
			services.ConfigureDatabase(Configuration);
			services.ConfigureScopedServices();
			services.ConfigureRouting();
			services.ConfigureLocalisation();
			services.AddMediator();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionsSnapshot<AppSettings> settings, UserManager<CoreWikiUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			app.ConfigureTelemetry();
			app.ConfigureExceptions(env);
			app.ConfigureSecurityHeaders();
			app.ConfigureRouting();
			app.ConfigureDatabase();
			await app.ConfigureAuthentication(userManager, roleManager);
			app.ConfigureRSSFeed(settings);
			app.ConfigureLocalisation();

			app.UseStatusCodePagesWithReExecute("/HttpErrors/{0}");
			app.UseMvc();
		}

	}
}
