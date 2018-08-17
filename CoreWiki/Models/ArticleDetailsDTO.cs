using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace CoreWiki.Models
{
	public class ArticleDetailsDTO
	{
		public int Id { get; set; }
		public Guid AuthorId { get; set; }
		public string Slug { get; set; }
		public string Topic { get; set; }
		public string Content { get; set; }
		public Instant Published { get; set; }
		public int ViewCount { get; set; }
		public int Version { get; set; }

		public IReadOnlyCollection<CommentDTO> Comments { get; set; }

		public static ArticleDetailsDTO FromDomain(Core.Domain.Article article) {

			var comments = (
			from comment in article.Comments
			select new CommentDTO
			{
				ArticleId = comment.Id,
				DisplayName = comment.DisplayName,
				Email = comment.Email,
				Content = comment.Content,
				Submitted = comment.Submitted
			}
		).ToList();

			return new ArticleDetailsDTO
			{
				Id = article.Id,
				AuthorId = article.AuthorId,
				Slug = article.Slug,
				Topic = article.Topic,
				Content = article.Content,
				Published = article.Published,
				Version = article.Version,
				ViewCount = article.ViewCount,
				Comments = comments
			};

		}

	}
}
