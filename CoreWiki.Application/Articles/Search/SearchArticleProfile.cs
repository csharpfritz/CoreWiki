using AutoMapper;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Search
{
	public class SearchArticleProfile: Profile
	{
		public SearchArticleProfile()
		{
			CreateMap<Article, ArticleSearchDto>();
		}
	}
}
