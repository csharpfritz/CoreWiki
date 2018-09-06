using System;
using NodaTime;

namespace CoreWiki.Application.Articles.Managing.Dto
{
	public class ArticleManageDto
	{
		public string Content { get; set; }
		public int Id { get; set; }
		public string Slug { get; set; }
		public string Topic { get; set; }
		public int Version { get; set; }
		public int ViewCount { get; set; }
		public CommentDto[] Comments { get; set; }

		public Instant Published { get; set; }
		public Guid AuthorId { get; set; }
		public SlugHistoryDto[] History { get; set; }
		public string AuthorName { get; set; }
	}
}
