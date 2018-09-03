using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Common.MappingProfiles
{
	public class ArticleManagingProfile: Profile
	{
		public ArticleManagingProfile()
		{
			CreateMap<CreateNewArticleCommand, Article>()
				.ForMember(d => d.Id, m=> m.Ignore())
				.ForMember(d => d.Version, m => m.UseValue(1))
				.ForMember(d => d.Published, m => m.Ignore())
				.ForMember(d => d.Comments, m => m.Ignore())
				.ForMember(d => d.History, m => m.Ignore())
				.ForMember(d => d.ViewCount, m => m.UseValue(0))
				;
		}
	}
}
