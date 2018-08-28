using AutoMapper;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Core.Domain;
using CoreWiki.ViewModels;
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
				cfg.CreateMap<Article, ArticleDetails>();
				cfg.CreateMap<Article, ArticleDelete>();
				cfg.CreateMap<CreateNewArticleCommand, Article>().ConvertUsing(o => new Article
				{
					AuthorId = o.AuthorId,
					AuthorName = o.AuthorName,
					Content = o.Content,
					Slug = o.Slug,
					Topic = o.Topic,
				});
			});

			config.AssertConfigurationIsValid();
			var mapper = config.CreateMapper();

			services?.AddSingleton(mapper);

			return mapper;

		}
	}
}
