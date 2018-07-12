namespace CoreWiki.Notifications.Models
{
    public class NewCommentEmailModel
    {
        public string BaseUrl { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string ArticleTopic { get; set; }
        public string ArticleUrl { get; set; }
        public string CommenterDisplayName { get; set; }
    }
}
