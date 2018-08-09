using CoreWiki.Core.Interfaces;
using CoreWiki.Data;
using CoreWiki.Data.Data.Repositories;
using CoreWiki.Data.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static partial class ConfigurationExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
		{
			services.AddEntityFrameworkSqlite()
				.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlite(config.GetConnectionString("CoreWikiData")));

			// db repos
			services.AddTransient<IArticleRepository, ArticleSqliteRepository>();
			services.AddTransient<ICommentRepository, CommentSqliteRepository>();
			services.AddTransient<ISlugHistoryRepository, SlugHistorySqliteRepository>();

			return services;
		}

		public static IApplicationBuilder ConfigureDatabase(this IApplicationBuilder app)
		{
			var scope = app.ApplicationServices.CreateScope();

			var identityContext = scope.SeedData()
				.ServiceProvider.GetService<CoreWikiIdentityContext>();
			CoreWikiIdentityContext.SeedData(identityContext);

			return app;
		}
	}
}
