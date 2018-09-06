using System.Collections.Generic;
using NodaTime;

namespace CoreWiki.ViewModels
{
	public class ArticleHistory
	{
		public string Topic { get; set; }
		public int Version { get; set; }
		public string AuthorName { get; set; }
		public Instant Published { get; set; }
		public IReadOnlyCollection<ArticleHistoryDetail> History { get; set; }
	}
}
