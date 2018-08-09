using CoreWiki.Core.Interfaces;
using CoreWiki.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Data
{

	public static class StartupExtensions
	{

		/// <summary>
		/// Configure the SQLite repositories and EntityFramework context to support it
		/// </summary>
		/// <param name="services"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static IServiceCollection AddSqliteRepositories(this IServiceCollection services, IConfiguration config) {

			services.AddEntityFrameworkSqlite()
			.AddDbContextPool<ApplicationDbContext>(options =>
				options.UseSqlite(config.GetConnectionString("CoreWikiData"))
					.EnableSensitiveDataLogging(true)
			);

			// db repos
			services.AddTransient<IArticleRepository, ArticleSqliteRepository>();
			services.AddTransient<ICommentRepository, CommentSqliteRepository>();
			services.AddTransient<ISlugHistoryRepository, SlugHistorySqliteRepository>();

			return services;

		}

		public static IServiceScope SeedData(this IServiceScope serviceScope) {

			var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

			ApplicationDbContext.SeedData(context);

			return serviceScope;

		}

	}

}
