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

			services.AddEntityFrameworkSqlite()
				.AddDbContextPool<ApplicationDbContext>(options =>
						options.UseSqlite("Data Source=./wiki.db")
				);

			// Add NodaTime clock for time-based testing
			services.AddSingleton<IClock>(SystemClock.Instance);

			services.AddRouting(options => options.LowercaseUrls = true);

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
				// app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseStaticFiles();

			app.UseMvc();

			var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
			ApplicationDbContext.SeedData(context);

		}

	}
}
