namespace CoreWiki.Extensibility.Common.Events
{
    public class PreCommentCreateEventArgs : CoreWikiModuleValidationEventArgs
    {
        public PreCommentCreateEventArgs(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}