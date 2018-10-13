namespace CoreWiki.Core.Domain
{
	public abstract class BaseArticle
	{
		public int Id { get; set; }
		public string Slug { get; set; }
		public string Topic { get; set; }

		public string Content { get; set; }
	}
}
