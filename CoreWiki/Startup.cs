using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Models;
using CoreWiki.SearchEngines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NodaTime;
using Snickler.RSSCore;
using Snickler.RSSCore.Providers;
using Snickler.RSSCore.Extensions;
using Snickler.RSSCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server.Features;
using CoreWiki.Configuration;
using Microsoft.Extensions.Options;
using CoreWiki.Helpers;
using Microsoft.ApplicationInsights.Extensibility;
using CoreWiki.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;

namespace CoreWiki
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRSSFeed<RSSProvider>();

			services.Configure<AppSettings>(Configuration);

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddEntityFrameworkSqlite()
				.AddDbContextPool<ApplicationDbContext>(options =>
					options.UseSqlite("Data Source=./wiki.db")
				);


			// Add NodaTime clock for time-based testing
			services.AddSingleton<IClock>(SystemClock.Instance);

			services.AddScoped<IArticlesSearchEngine, ArticlesDbSearchEngine>();

			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddHttpContextAccessor();

			services.AddLocalization(options => options.ResourcesPath = "Globalization");

			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization()
				.AddRazorPagesOptions(options =>
				{
					options.Conventions.AddPageRoute("/Edit", "/{Slug}/Edit");
					options.Conventions.AddPageRoute("/Delete", "{Slug}/Delete");
					options.Conventions.AddPageRoute("/Details", "{Slug?}");
					options.Conventions.AddPageRoute("/Details", @"Index");
				});

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddProgressiveWebApp();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptionsSnapshot<AppSettings> settings)
		{

      var initializer = new ArticleNotFoundInitializer();

      var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
      configuration.TelemetryInitializers.Add(initializer);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			} else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseRSSFeed("/feed", new RSSFeedOptions
			{
				Title = "CoreWiki RSS Feed",
				Copyright = DateTime.UtcNow.Year.ToString(),
				Description = "RSS Feed for CoreWiki",
				Url = settings.Value.Url
			});

			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),
			});

			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
			var identityContext = scope.ServiceProvider.GetService<CoreWikiIdentityContext>();

			app.UseStatusCodePagesWithReExecute("/HttpErrors/{0}");

			app.UseMvc();
			ApplicationDbContext.SeedData(context);
			CoreWikiIdentityContext.SeedData(identityContext);
		}

	}
}
