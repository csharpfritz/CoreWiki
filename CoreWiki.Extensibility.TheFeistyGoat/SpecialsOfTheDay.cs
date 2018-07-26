using CoreWiki.Extensibility.Common;
using CoreWiki.Extensibility.Common.Events;
using System;
using System.Text;

namespace CoreWiki.Extensibility.TheFeistyGoat
{
	public class SpecialsOfTheDay : ICoreWikiModule
	{
		public void Initialize(ICoreWikiModuleHost coreWikiModuleHost)
		{

			coreWikiModuleHost.Events.PreCreateArticle += BeforeArticleCreated;

		}

		private void BeforeArticleCreated(PreArticleCreateEventArgs obj)
		{
			// get specials from a data store
			var specials = SpecialItem.GetSpecials();

			StringBuilder builder = new StringBuilder();
			builder.AppendLine();
			builder.AppendLine("------- The Feisty Goat :: daily specials -------");
			foreach (var item in specials)
				builder.AppendLine(string.Format("{0} - regular price {1:#.00}, today: {2:#.00}", item.Item, item.RegularPrice, item.SpecialPrice));

			obj.Content += builder.ToString();
		}
	}
}
