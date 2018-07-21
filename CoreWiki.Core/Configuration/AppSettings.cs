using System;
using CoreWiki.Extensibility.Common;

namespace CoreWiki.Core.Configuration
{
	public class AppSettings
	{

		public Uri Url { get; set; }
		public Connectionstrings ConnectionStrings { get; set; }
		public Comments Comments { get; set; }
		public EmailNotifications EmailNotifications { get; set; }
		public CspSettings CspSettings { get; set; }

        public ExtensibilityModulesConfig[] ExtensibilityModules { get; set; } // MAC

    }
}
