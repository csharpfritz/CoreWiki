using AutoMapper;
using CoreWiki.Application.Articles.Reading.Commands;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Reading
{
	public class ArticleReadingProfile: Profile
	{

		public ArticleReadingProfile()
		{
			CreateMap<Comment, CommentDto>();
			CreateMap<CreateCommentDto, Comment>()
				.ForMember(d => d.Id, m => m.Ignore());
			CreateMap<CreateNewCommentCommand, CreateCommentDto>();
			CreateMap<ArticleHistory, ArticleHistoryDto>();
			CreateMap<Article, ArticleReadingDto>()
				.ForMember(d => d.ArticleHistory, m => m.MapFrom(s => s.History))
				.ForMember(d => d.SlugHistory, m => m.MapFrom(s => s.SlugHistory));
			CreateMap<SlugHistory, SlugHistoryDto>()
				.ForMember( d => d.Version, m => m.MapFrom(s => s.Article.Version))
				.ForMember(d => d.Content, m => m.MapFrom(s => s.Article.Content))
				.ForMember(d => d.Published, m => m.MapFrom(s => s.Article.Published));
			CreateMap<CommentDto, Comment>();
		}
	}
}
