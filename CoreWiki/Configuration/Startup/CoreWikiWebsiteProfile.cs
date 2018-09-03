using AutoMapper;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Application.Articles.Services.Dto;
using CoreWiki.ViewModels;

namespace CoreWiki.Configuration.Startup
{
	public class CoreWikiWebsiteProfile : Profile
	{
		public CoreWikiWebsiteProfile()
		{
			CreateMap<ArticleReadingDto, ArticleDetails>();
			CreateMap<ArticleReadingDto, ArticleDelete>();
			CreateMap<ArticleReadingDto, ArticleEdit>();
			CreateMap<Comment, CreateNewCommentCommand>()

				.ForMember(d => d.AuthorId, m => m.Ignore());
		}
	}
}
