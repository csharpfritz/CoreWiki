using NodaTime;
using System;

namespace CoreWiki.Core.Domain
{
	public class ArticleHistory
	{
		public int Id { get; set; }

		public Guid AuthorId { get; set; }

		public int Version { get; set; }

		public string Topic { get; set; }

		public string Slug { get; set; }

		public Instant Published { get; set; }

		public string Content { get; set; }

		public int ArticleId { get; set; }

		public static ArticleHistory FromArticle(Article article)
		{

			return new ArticleHistory
			{
				ArticleId = article.Id,
				AuthorId = article.AuthorId,
				Content = article.Content,
				Published = article.Published,
				Slug = article.Slug,
				Topic = article.Topic,
				Version = article.Version
			};

		}

	}


}
