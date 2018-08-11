using System;

namespace CoreWiki.Application
{
	public class ArticleCreateDTO
	{
		public string Topic { get; set; }
		public string Content { get; set; }

		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }

		public string Slug { get; set; }
		
	}
}

