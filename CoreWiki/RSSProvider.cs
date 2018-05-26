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

namespace CoreWiki
{
  public class RSSProvider : IRSSProvider
  {
	private ApplicationDbContext _context;

    public IConfiguration Configuration { get; }

    public RSSProvider(ApplicationDbContext context, IConfiguration config)
	{
	  _context = context;
		Configuration = config;

	}

	public async Task<IList<RSSItem>> RetrieveSyndicationItems()
	{
	  var articles = await _context.Articles.OrderByDescending(a => a.Published).Take(10).ToListAsync();
	  return articles.Select(rssItem =>
	  {
		var wikiItem = new RSSItem
		{
		  Content = rssItem.Content,  // TODO: May need to truncate for VERY large articles in the future
			//Will probably need FQDN for a permalink in RSS. May have to use _httpContextAccessor.HttpContext.Request.Host.Host.
			//in Startup.cs, add _services.AddHttpContextAccessor();
			PermaLink = new Uri($"{Configuration["Url"]}/{rssItem.Slug}"),
		  LinkUri =		new Uri($"{Configuration["Url"]}/{rssItem.Slug}"),
		  PublishDate = rssItem.PublishedDateTime,
		  LastUpdated = rssItem.PublishedDateTime,
		  Title = rssItem.Topic

		};

		wikiItem.Authors.Add("Jeff Fritz"); // TODO: Grab from user who saved record... not this guy
		return wikiItem;
	  }).ToList();
	}

  }
}
