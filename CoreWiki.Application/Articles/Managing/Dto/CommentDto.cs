using System;
using NodaTime;

namespace CoreWiki.Application.Articles.Managing.Dto
{
	public class CommentDto
	{
		public int ArticleId { get; set; }
		public Instant Submitted { get; set; }
		public Guid AuthorId { get; set; }
		public string Content { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public int Id { get; set; }
	}
}
