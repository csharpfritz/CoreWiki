using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Queries
{

	public class GetArticlesToCreateFromArticleHandler : IRequestHandler<GetArticlesToCreateFromArticle, string[]>
	{

		public GetArticlesToCreateFromArticleHandler(IArticleRepository articleRepository)
		{
			this.ArticleRepository = articleRepository;
		}

		public IArticleRepository ArticleRepository { get; }

		public async Task<string[]> Handle(GetArticlesToCreateFromArticle request, CancellationToken cancellationToken)
		{

			return (await GetArticlesToCreate(request.Slug)).ToArray();

		}

		private static readonly string articleLinksPattern = @"(\[[\w\s.\-_:;\!\?]*[\]][\(])((?!(http|https))[\w\s\-_]*)([\)])";
		private static readonly string LinkPrefix = "](";

		private async Task<IList<string>> GetArticlesToCreate(string slug)
		{
			var articlesToCreate = new List<string>();
			var thisArticle = await ArticleRepository.GetArticleBySlug(slug);

			if (!string.IsNullOrWhiteSpace(thisArticle.Content))
			{
				var internalWikiLinkArray = FindWikiArticleLinks(thisArticle.Content);
				foreach (var link in internalWikiLinkArray)
				{
					// Normalise the potential new wiki link into our slug format
					var newSlug = link;

					// Does the slug already exist in the database?
					if (!await ArticleRepository.IsTopicAvailable(slug, thisArticle.Id))
					{
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
