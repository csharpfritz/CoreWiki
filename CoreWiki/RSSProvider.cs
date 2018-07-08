using CoreWiki.Configuration;
using CoreWiki.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Snickler.RSSCore.Models;
using Snickler.RSSCore.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki
{
	public class RSSProvider : IRSSProvider
	{
		private readonly ApplicationDbContext _context;
		private readonly Uri baseURL;

		public RSSProvider(ApplicationDbContext context, IOptionsSnapshot<AppSettings> settings)
		{
			_context = context;
			baseURL = settings.Value.Url;
		}

		public async Task<IList<RSSItem>> RetrieveSyndicationItems()
		{
			var articles = await _context.Articles.OrderByDescending(a => a.Published).Take(10).ToListAsync();
			return articles.Select(rssItem =>
			{
				var absoluteURL = new Uri(baseURL, $"/{rssItem.Slug}");

				var wikiItem = new RSSItem
				{
					Content = rssItem.Content,
					PermaLink = absoluteURL,
					LinkUri = absoluteURL,
					PublishDate = rssItem.Published.ToDateTimeUtc(),
					LastUpdated = rssItem.Published.ToDateTimeUtc(),
					Title = rssItem.Topic,
				};

				wikiItem.Authors.Add("Jeff Fritz"); // TODO: Grab from user who saved record... not this guy
				return wikiItem;
			}).ToList();
		}

	}
}
