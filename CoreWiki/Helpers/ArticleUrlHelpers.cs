using CoreWiki.Application.Common;

/// <summary>
/// Small helper to get RedirectUrls for Articles
/// </summary>
namespace CoreWiki.Helpers
{
	public static class ArticleUrlHelpers
	{
		public static string GetUrlOrHome(string slug)
		{
			return $"/wiki/{(slug == Constants.HomePageSlug ? "" : slug)}";
		}

		public static string GetUrl(string slug)
		{
			return $"/wiki/{slug}";
		}
	}
}
