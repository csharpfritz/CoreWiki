using CoreWiki.Extensibility.Common;
using CoreWiki.Extensibility.Common.Events;
using System;

namespace CoreWiki.Extensibility.TheFeistyGoat
{
	public class PostToTwitter : ICoreWikiModule
	{
		public void Initialize(ICoreWikiModuleHost coreWikiModuleHost)
		{
			coreWikiModuleHost.Events.PostCreateArticle += OnArticleSubmitted;
			coreWikiModuleHost.Events.PostEditArticle += OnArticleEdited;

		}

		private void OnArticleEdited(PostArticleEditEventArgs obj)
		{
			throw new NotImplementedException();
		}

		private void OnArticleSubmitted(PostArticleCreateEventArgs obj)
		{
			throw new NotImplementedException();
		}

	}
}
