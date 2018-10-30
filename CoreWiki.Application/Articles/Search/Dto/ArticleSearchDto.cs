using System;
using NodaTime;

namespace CoreWiki.Application.Articles.Search.Dto
{
	public class ArticleSearchDto
	{
		public bool IsHomePage { get; set; }
		public string Content { get; set; }
		public int Id { get; set; }
		public string Slug { get; set; }
		public string Topic { get; set; }
		public int Version { get; set; }
		public int ViewCount { get; set; }

		public Instant Published { get; set; }
		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }
	}
}
