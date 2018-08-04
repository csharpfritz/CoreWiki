using System.Collections.Generic;
using NodaTime;

namespace CoreWiki.Data.Models
{
	public class ArticleHistoryDto
	{
		public string Topic { get; set; }
		public int Version { get; set; }
		public string AuthorName { get; set; }
		public Instant Published { get; set; }
		public IReadOnlyCollection<ArticleHistoryDetailDto> History { get; set; }
	}

	public class ArticleHistoryDetailDto
	{
		public int Version { get; set; }
		public string AuthorName { get; set; }
		public Instant Published { get; set; }
	}
}
