using NodaTime;

namespace CoreWiki.Data.Models
{
	public class ArticleDeleteDTO
	{
		public string Topic { get; set; }
		public string Content { get; set; }
		public Instant Published { get; set; }
	}
}
