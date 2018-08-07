using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Core.Domain
{
	public class Article
	{

		public int Id { get; set; }

		public string Topic { get; set; }

		public string Slug { get; set; }

		public int Version { get; set; } = 1;

		public Instant Published { get; set; }

		public Guid AuthorId { get; set; }

		public string AuthorName { get; set; } = "Unknown";

		public string Content { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<ArticleHistory> History { get; set; }

		public Article()
		{
			this.Comments = new HashSet<Comment>();
			this.History = new HashSet<ArticleHistory>();
		}

		public int ViewCount { get; set; } = 0;

	}

}
