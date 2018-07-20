namespace CoreWiki.Core.Configuration
{
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
}
