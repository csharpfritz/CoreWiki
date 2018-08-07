using NodaTime;

namespace CoreWiki.Models
{
	public class ArticleSummaryDTO
	{
		public string Slug { get; set; }
		public string Topic { get; set; }
		public Instant Published { get; set; }
		public int ViewCount { get; set; }
	}
}
