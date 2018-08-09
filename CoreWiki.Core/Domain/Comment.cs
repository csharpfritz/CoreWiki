using NodaTime;
using System;

namespace CoreWiki.Core.Domain
{
	public class Comment
	{

		public int Id { get; set; }

		public int IdArticle { get; set; }

		public string DisplayName { get; set; }

		public string Email { get; set; }

		public Instant Submitted { get; set; }

		public Guid AuthorId { get; set; }

		public string Content { get; set; }

	}

}
