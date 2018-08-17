using CoreWiki.Core.Domain;
using NodaTime;
using System;

namespace CoreWiki.Models
{
	public class CommentDTO
	{
		public int ArticleId { get; set; }
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string Content { get; set; }
		public Instant Submitted { get; set; }

	}
}
