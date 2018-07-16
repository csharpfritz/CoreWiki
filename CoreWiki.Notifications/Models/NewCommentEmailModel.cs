namespace CoreWiki.Notifications.Models
{
    public class NewCommentEmailModel : EmailMessageBaseModel
    {
        public string AuthorName { get; set; }
        public string ArticleTopic { get; set; }
        public string ArticleUrl { get; set; }
        public string CommenterDisplayName { get; set; }
    }
}
