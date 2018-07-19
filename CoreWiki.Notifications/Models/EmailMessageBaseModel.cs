namespace CoreWiki.Notifications.Models
{
    public abstract class EmailMessageBaseModel
    {
        public string BaseUrl { get; set; }
        public string Title { get; set; }
    }
}
