using System;
using System.Collections.Generic;
using NodaTime;

namespace CoreWiki.Data.Models
{
	public class ArticleDetailsDTO
	{
		public int Id { get; set; }
		public Guid AuthorId { get; set; }
		public string Slug { get; set; }
		public string Topic { get; set; }
		public string Content { get; set; }
		public Instant Published { get; set; }
		public int ViewCount { get; set; }
		public int Version { get; set; }

		public IReadOnlyCollection<CommentDTO> Comments { get; set; }
	}
}
