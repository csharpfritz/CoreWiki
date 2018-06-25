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
		public CspSettings CspSettings { get; set; }

	}

	public class CspSettings
	{
		public string[] ImageSources { get; set; }
		public string[] StyleSources { get; set; }
		public string[] ScriptSources { get; set; }
		public string[] FontSources { get; set; }
		public string[] FormActions { get; set; }
		public string[] FrameAncestors { get; set; }
		public string[] ReportUris { get; set; }
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
}
