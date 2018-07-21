using System;

namespace CoreWiki.Extensibility.Common
{
    public class CoreWikiModuleEvents
    {
        public Action<PreSubmitArticleEventArgs> PreSubmitArticle;
        public Action<ArticleSubmittedEventArgs> ArticleSubmitted;
    }
}