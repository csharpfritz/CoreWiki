namespace CoreWiki.Notifications.Models
{
    public class ForgotPasswordEmailModel : EmailMessageBaseModel
    {
        public string ReturnUrl { get; set; }
        public string AccountEmail { get; set; }
    }
}
