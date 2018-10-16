using CoreWiki.Application.Common;
using CoreWiki.Helpers;
using NodaTime;

namespace CoreWiki.ViewModels
{
	public class ArticleSummary
	{
		public string Slug { get; set; }
		public string Topic { get; set; }
		public Instant Published { get; set; }
		public int ViewCount { get; set; }

		public string Url => ArticleUrlHelpers.GetUrlOrHome(Slug);
		public bool IsNotHomePage => Slug != Constants.HomePageSlug;
	}
}
