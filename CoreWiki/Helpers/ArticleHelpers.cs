using CoreWiki.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoreWiki.Helpers
{
	public static class ArticleHelpers
    {
		private static readonly string articleLinksPattern = @"(\[[\w\s.\-_:;\!\?]*[\]][\(])((?!(http|https))[\w\s\-_]*)([\)])";
		private static readonly string LinkPrefix = "](";

		public static IList<string> GetArticlesToCreate(ApplicationDbContext context, Article article, bool createSlug = false)
		{
			var articlesToCreate = new List<string>();
			var internalWikiLinkArray = FindWikiArticleLinks(article.Content);
			foreach (var link in internalWikiLinkArray)
			{
				// Normalise the potential new wiki link into our slug format
				var slug = createSlug ? UrlHelpers.URLFriendly(link) : link;

				// Does the slug already exist in the database?
				if (!context.Articles.Any(x => x.Slug.Equals(slug) && x.Id != article.Id))
				{
					if (createSlug && !slug.Equals(link))
					{
						var target = LinkPrefix + link;
						var replacement = LinkPrefix + slug;
						article.Content = article.Content.Replace(target, replacement);
					}

					articlesToCreate.Add(slug);
				}
			}

			return articlesToCreate.Distinct().ToList();

			IEnumerable<string> FindWikiArticleLinks(string content)
			{
				var matches = Regex.Matches(content, articleLinksPattern)
					.Cast<Match>()
					.Select(match => match.Groups[2].Value)
					.ToArray();

				return matches;
			}
		}
	}
}
