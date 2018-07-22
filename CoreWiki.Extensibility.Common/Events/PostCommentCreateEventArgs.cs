namespace CoreWiki.Extensibility.Common.Events
{
    public class PostCommentCreateEventArgs : CoreWikiModuleEventArgs
    {
        public PostCommentCreateEventArgs(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}