using System;
using CoreWiki.Core.Domain;
using NodaTime;

namespace CoreWiki.Application.Articles.Managing.Dto
{
	public class ArticleManageDto : BaseArticle
	{
		public int Version { get; set; }
		public int ViewCount { get; set; }
		public CommentDto[] Comments { get; set; }

		public Instant Published { get; set; }
		public Guid AuthorId { get; set; }
		public SlugHistoryDto[] History { get; set; }
		public string AuthorName { get; set; }
	}
}
