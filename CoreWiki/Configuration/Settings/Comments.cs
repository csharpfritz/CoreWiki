namespace CoreWiki.Configuration.Settings
{
	public class Comments
	{
		public CommentsEngine Engine { get; set; }
		public Disqus Disqus { get; set; }
		public bool IsEngineLocal => Engine == CommentsEngine.Local;
	}
}
