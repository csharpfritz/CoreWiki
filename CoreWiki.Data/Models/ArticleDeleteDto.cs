using NodaTime;

namespace CoreWiki.Data.Models
{
	public class ArticleDeleteDto
	{
		public string Topic { get; set; }
		public string Content { get; set; }
		public Instant Published { get; set; }
	}
}
