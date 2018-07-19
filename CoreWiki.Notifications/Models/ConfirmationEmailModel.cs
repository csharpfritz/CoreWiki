namespace CoreWiki.Notifications.Models
{
    public class ConfirmationEmailModel : EmailMessageBaseModel
    {
        public string ReturnUrl { get; set; }
        public string ConfirmEmail { get; set; }
    }
}
