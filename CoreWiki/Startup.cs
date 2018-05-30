using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Models;
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

namespace CoreWiki
{
	public static class StaticHttpContextExtensions
	{
		public static void AddHttpContextAccessor(this IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}

		public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
		{
			var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
			WikiHttpContext.HttpContext.Configure(httpContextAccessor);
			return app;
		}
	}
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

			services.AddRouting(options => options.LowercaseUrls = true);
			services.AddHttpContextAccessor();

			services.AddMvc()
				.AddRazorPagesOptions(options =>
				{
					options.Conventions.AddPageRoute("/Details", "{Slug?}");
					options.Conventions.AddPageRoute("/Details", @"Index");
				});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseStaticHttpContext();

			app.UseRSSFeed("/feed", new RSSFeedOptions
			{
				Title = "CoreWiki RSS Feed",
				Copyright = DateTime.UtcNow.Year.ToString(),
				Description = "RSS Feed for CoreWiki",
				Url = new Uri(Configuration["Url"])
			});

			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

			app.UseMvc();
			ApplicationDbContext.SeedData(context);

		}

	}
}
