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

				cfg.CreateMap<ViewModels.Comment, Core.Domain.Comment>()
					.ForMember(dst => dst.IdArticle, opt => opt.MapFrom(src => src.ArticleId))
					.ForMember(dst => dst.Id, opt => opt.Ignore())
					.ForMember(dst => dst.AuthorId, opt => opt.Ignore());

				cfg.CreateMap<CreateNewArticleCommand, Article>().ConvertUsing(o => new Article
				{
					AuthorId = o.AuthorId,
					AuthorName = o.AuthorName,
					Content = o.Content,
					Slug = o.Slug,
					Topic = o.Topic,
				});

				cfg.CreateMap<CreateNewCommentCommand, Core.Domain.Comment>().ConvertUsing(o => new Core.Domain.Comment
				{
					IdArticle = o.Article.Id,
					AuthorId = o.Comment.AuthorId,
					Content = o.Comment.Content,
					DisplayName =o.Comment.DisplayName,
					Email = o.Comment.Email,
					Submitted = o.Comment.Submitted,
					Id = o.Comment.Id
				});
			});

			config.AssertConfigurationIsValid();
			var mapper = config.CreateMapper();

			services?.AddSingleton(mapper);

			return mapper;

		}
	}
}
