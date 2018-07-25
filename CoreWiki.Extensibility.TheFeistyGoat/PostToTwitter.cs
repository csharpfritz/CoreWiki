using CoreWiki.Extensibility.Common;
using System;

namespace CoreWiki.Extensibility.TheFeistyGoat
{
    public class PostToTwitter : ICoreWikiModule
    {
        void ICoreWikiModule.Initialize(CoreWikiModuleEvents moduleEvents)
        {
            moduleEvents.ArticleSubmitted += OnArticleSubmitted;
        }

        void OnArticleSubmitted(ArticleSubmittedEventArgs e)
        {
            /*
             * Pesudo-code example using TweetSharp
             * 
            var twitterService = new TwitterService("key", "secret");

            SendTweetOptions options = new SendTweetOptions();
            options.Status = "The Feisty Goat has just published a new article to its Wiki!";
            
            twitterService.SendTweet(options);
            */
        }
    }
}
