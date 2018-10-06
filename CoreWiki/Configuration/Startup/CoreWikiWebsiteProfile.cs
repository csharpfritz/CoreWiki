using System;
using System.Security.Claims;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Reading.Commands;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Application.Common;
using CoreWiki.Core.Common;
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
			CreateMap<ClaimsPrincipal, CreateNewCommentCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))));

			CreateMap<string, CreateNewArticleCommand>(MemberList.None)
				.ForMember(d => d.Content, m => m.UseValue(string.Empty))
				.ForMember(d => d.Slug, m => m.MapFrom(s => s))
				.ForMember(d => d.Topic, m => m.MapFrom(s => UrlHelpers.SlugToTopic(s)));
			CreateMap<ArticleCreate, CreateNewArticleCommand>(MemberList.Source)
				.ForMember(d => d.Slug, m => m.MapFrom(s => s.Content))
				.ForMember(d => d.Slug, m => m.MapFrom(s => UrlHelpers.URLFriendly(s.Topic)));
			CreateMap<ClaimsPrincipal, CreateNewArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))))
				.ForMember(d => d.AuthorName, m => m.MapFrom(s => s.Identity.Name));

			CreateMap<ArticleEdit, EditArticleCommand>(MemberList.Source);
			CreateMap<ClaimsPrincipal, EditArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))))
				.ForMember(d => d.AuthorName, m => m.MapFrom(s => s.Identity.Name));
		}
	}
}
