using System;

namespace CoreWiki.Extensibility.Common
{
    public class ArticleSubmittedEventArgs : EventArgs
    {
        public ArticleSubmittedEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
    }
}