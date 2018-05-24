using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snickler.RSSCore.Models;
using Snickler.RSSCore.Providers;

namespace CoreWiki
{
  public class RSSProvider : IRSSProvider
  {
	Task<IList<RSSItem>> IRSSProvider.RetrieveSyndicationItems()
	{
	  IList<RSSItem> syndicationList = new List<RSSItem>();
	  var RSSItem = new RSSItem()
	  {
		Content = "Page",
		PermaLink = new Uri("http://www.github.com/CSharpFritz/CoreWiki"),
		LinkUri = new Uri("http://www.github.com/CSharpFritz/CoreWiki/Item.aspx?123"),
		LastUpdated = DateTime.Now,
		PublishDate = DateTime.Now,
		Title = "Sample"

	  };
	  RSSItem.Categories.Add("Issues");
	  RSSItem.Authors.Add("Jeff Fritz");
	  syndicationList.Add((RSSItem)syndicationList);
	  return Task.FromResult(syndicationList);
		}
  }
}
