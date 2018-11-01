using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Snickler.RSSCore.Models;
using Snickler.RSSCore.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Queries;
using CoreWiki.Configuration.Settings;
using MediatR;

namespace CoreWiki
{
	public class RSSProvider : IRSSProvider
	{
		private readonly IMediator _mediator;
		private readonly Uri baseURL;

		public RSSProvider(IMediator mediator, IOptionsSnapshot<AppSettings> settings)
		{
			_mediator = mediator;
			baseURL = settings.Value.Url;
		}

		public async Task<IList<RSSItem>> RetrieveSyndicationItems()
		{
			var articles = await _mediator.Send(new GetLatestArticlesQuery(10));
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
