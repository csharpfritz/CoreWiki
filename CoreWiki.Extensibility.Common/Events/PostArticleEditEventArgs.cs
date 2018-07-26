namespace CoreWiki.Extensibility.Common.Events
{
    public class PostArticleEditEventArgs : CoreWikiModuleEventArgs
    {
        public PostArticleEditEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
    }
}