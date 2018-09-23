using System;
using CoreWiki.Notifications.Abstractions.Configuration;

namespace CoreWiki.Configuration.Settings
{
	public class AppSettings
	{
		public Uri Url { get; set; }
		public Connectionstrings ConnectionStrings { get; set; }
		public Comments Comments { get; set; }
		public EmailNotifications EmailNotifications { get; set; }
		public CspSettings CspSettings { get; set; }
	}
}
