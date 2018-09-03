using AutoMapper;
using CoreWiki.Application.Common.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWiki.Configuration.Startup
{
	public static class ConfigureAutomapperServices
	{

		public static IMapper ConfigureAutomapper(this IServiceCollection services) {

			var config = new MapperConfiguration(cfg => {
				cfg.AddProfile<ArticleReadingProfile>();
				cfg.AddProfile<ArticleManagingProfile>();
				cfg.AddProfile<CoreWikiWebsiteProfile>();
			});

			config.AssertConfigurationIsValid();
			var mapper = config.CreateMapper();

			services?.AddSingleton(mapper);

			return mapper;

		}
	}
}
