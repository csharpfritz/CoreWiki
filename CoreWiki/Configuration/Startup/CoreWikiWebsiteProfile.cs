using System;
using System.Collections.Generic;
using System.Security.Claims;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Reading.Commands;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Application.Common;
using CoreWiki.Data.EntityFramework.Security;
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

			CreateMap<ArticleReadingDto, ArticleHistory>()
				.ForMember(d => d.AuthorName, m => m.Ignore())
				.ForMember(d => d.History, m=> m.MapFrom(s => s.ArticleHistory))
				;

			CreateMap<Comment, CreateNewCommentCommand>()
				.ForMember(d => d.AuthorId, m => m.Ignore());
			CreateMap<ClaimsPrincipal, CreateNewCommentCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))));

			CreateMap<ArticleCreate, CreateNewArticleCommand>(MemberList.Source);
			CreateMap<ClaimsPrincipal, CreateNewArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))));
			CreateMap<CoreWikiUser, CreateNewArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.Id)));

			CreateMap<ClaimsPrincipal, CreateSkeletonArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))));


			CreateMap<ArticleEdit, EditArticleCommand>(MemberList.Source)
				.ForSourceMember(d => d.Slug, m => m.Ignore());
			CreateMap<ClaimsPrincipal, EditArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.FindFirstValue(ClaimTypes.NameIdentifier))));
			CreateMap<CoreWikiUser, EditArticleCommand>(MemberList.None)
				.ForMember(d => d.AuthorId, m => m.MapFrom(s => Guid.Parse(s.Id)));

			CreateMap<IList<ArticleReadingDto>, SearchResultDto<ArticleSummary>>(MemberList.None)
				.ForMember(d => d.Results, m => m.MapFrom(s => s))
				.ForMember(d => d.TotalResults, m => m.MapFrom(s => s.Count));
		}
	}
}
