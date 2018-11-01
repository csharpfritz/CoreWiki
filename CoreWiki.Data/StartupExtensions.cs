using CoreWiki.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreWiki.Data.Abstractions.Interfaces;
using System;

namespace CoreWiki.Data.EntityFramework
{

	public static class StartupExtensions
	{

		/// <summary>
		/// Configure the SQLite repositories and EntityFramework context to support it
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration config) {

			Action<DbContextOptionsBuilder> optionsBuilder;
			var connectionString = config.GetConnectionString("CoreWikiData");

			switch (config["DataProvider"].ToLowerInvariant()) {
				case "postgres":
					services.AddEntityFrameworkNpgsql();
					optionsBuilder = options => options.UseNpgsql(connectionString);
					break;
				default:
					services.AddEntityFrameworkSqlite();
					connectionString = !string.IsNullOrEmpty(connectionString) ? connectionString : "DataSource=./App_Data/wikiContent.db";
					optionsBuilder = options => options.UseSqlite(connectionString);
					break;
			}

			services.AddDbContextPool<ApplicationDbContext>(options => {
				optionsBuilder(options);
				options.EnableSensitiveDataLogging();
			});

			// db repos
			services.AddTransient<IArticleRepository, ArticleRepository>();
			services.AddTransient<ICommentRepository, CommentRepository>();
			services.AddTransient<ISlugHistoryRepository, SlugHistoryRepository>();
			return services;

		}

		public static IServiceScope SeedData(this IServiceScope serviceScope) {

			var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

			ApplicationDbContext.SeedData(context);

			return serviceScope;

		}

	}

}
