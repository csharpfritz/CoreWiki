namespace CoreWiki.Extensibility.Common.Events
{
    public class PreArticleCreateEventArgs : CoreWikiModuleValidationEventArgs
    {
        public PreArticleCreateEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
    }
}