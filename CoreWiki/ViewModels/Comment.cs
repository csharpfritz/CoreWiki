using CoreWiki.Core.Domain;
using NodaTime;
using System;

namespace CoreWiki.ViewModels
{
	public class Comment
	{
		public int ArticleId { get; set; }
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string Content { get; set; }
		public Instant Submitted { get; set; }

	}
}
