using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Configuration
{
	public class AppSettings
	{

		public Uri Url { get; set; }
		public Connectionstrings ConnectionStrings { get; set; }
		public Comments Comments { get; set; }
		public EmailNotifications EmailNotifications { get; set; }
	}

	public class Connectionstrings
	{
		public string CoreWikiIdentityContextConnection { get; set; }
	}

	public enum CommentsEngine
	{
		Local = 0,
		Disqus = 1
	}

	public class Comments
	{
		public CommentsEngine Engine { get; set; }
		public Disqus Disqus { get; set; }
		public bool IsEngineLocal => Engine == CommentsEngine.Local;
	}

	public class Disqus
	{
		public string ShortName { get; set; }

	}

	public class EmailNotifications
	{
		public string SendGridApiKey { get; set; }
		public string FromEmailAddress { get; set; }
		public string FromName { get; set; }
	}
}
