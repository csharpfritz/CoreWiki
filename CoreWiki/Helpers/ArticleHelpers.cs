using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CoreWiki.Helpers
{

	// TODO: Refactor this into an ArticleApplicationService
	// TODO: Name this thing 

	public static class ArticleHelpers
	{
		private static readonly string articleLinksPattern = @"(\[[\w\s.\-_:;\!\?]*[\]][\(])((?!(http|https))[\w\s\-_]*)([\)])";
		private static readonly string LinkPrefix = "](";

		public static async Task<IList<string>> GetArticlesToCreate(IArticleRepository articleRepo, Article article, bool createSlug = false)
		{
			var articlesToCreate = new List<string>();

			if (!string.IsNullOrWhiteSpace(article.Content))
			{
				var internalWikiLinkArray = FindWikiArticleLinks(article.Content);
				foreach (var link in internalWikiLinkArray)
				{
					// Normalise the potential new wiki link into our slug format
					var slug = createSlug ? UrlHelpers.URLFriendly(link) : link;

					// Does the slug already exist in the database?
					if (!await articleRepo.IsTopicAvailable(slug, article.Id))
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
