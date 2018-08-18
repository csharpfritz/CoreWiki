using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Configuration.Startup
{
	public static class ConfigureAutomapperServices
	{

		public static IMapper ConfigureAutomapper(this IServiceCollection services) {

			var config = new MapperConfiguration(cfg => {
				cfg.CreateMap<Core.Domain.Article, ViewModels.ArticleDetails>();
			});

			config.AssertConfigurationIsValid();
			var mapper = config.CreateMapper();

			services?.AddSingleton(mapper);

			return mapper;

		}

	}
}
