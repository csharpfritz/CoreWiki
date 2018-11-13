using NodaTime;
using System;

namespace CoreWiki.Application.Articles.Reading.Dto
{
	public class ArticleHistoryDto {

		public int Id { get; set; }

		public Guid AuthorId { get; set; }

		public int Version { get; set; }

		public Instant Published { get; set; }

		public string Content { get; set; }

	}

}
