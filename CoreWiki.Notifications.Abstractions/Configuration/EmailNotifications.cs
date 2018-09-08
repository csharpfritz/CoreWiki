namespace CoreWiki.Notifications.Abstractions.Configuration
{

	public class EmailNotifications
	{
		public string SendGridApiKey { get; set; }
		public string FromEmailAddress { get; set; }
		public string FromName { get; set; }
	}
}
