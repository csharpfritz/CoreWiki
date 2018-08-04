using System.Collections.Generic;
using NodaTime;

namespace CoreWiki.Data.Models
{
	public class ArticleHistoryDTO
	{
		public string Topic { get; set; }
		public int Version { get; set; }
		public string AuthorName { get; set; }
		public Instant Published { get; set; }
		public IReadOnlyCollection<ArticleHistoryDetailDTO> History { get; set; }
	}

	public class ArticleHistoryDetailDTO
	{
		public int Version { get; set; }
		public string AuthorName { get; set; }
		public Instant Published { get; set; }
	}
}
