using System.ComponentModel;

namespace CoreWiki.Extensibility.Common
{
    public class PreSubmitArticleEventArgs : CancelEventArgs
    {
        public PreSubmitArticleEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
        public string ModelErrorProperty { get; set; }
        public string ModelErrorMessage { get; set; }
    }
}