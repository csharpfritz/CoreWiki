using CoreWiki.Application.Common;
using CoreWiki.Core.Domain;

/// <summary>
/// Small helper to get RedirectUrls for Articles
/// </summary>
namespace CoreWiki.Helpers
{
	public static class ArticleUrlHelpers
	{
		public static string GetUrlOrHome(this BaseArticle from)
		{
			return GetUrlOrHome(from.Slug);
		}

		public static string GetUrl(this BaseArticle from)
		{
			return GetUrl(from.Slug);
		}

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
