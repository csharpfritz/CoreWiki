using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Snickler.RSSCore.Models;
using Snickler.RSSCore.Providers;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Snickler.RSSCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using CoreWiki.Configuration;

namespace CoreWiki
{
	public class RSSProvider: IRSSProvider
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
