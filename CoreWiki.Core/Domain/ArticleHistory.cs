using NodaTime;
using System;

namespace CoreWiki.Core.Domain
{
	public class ArticleHistory
	{
		public int Id { get; set; }

		public virtual Article Article { get; set; }

		public Guid AuthorId { get; set; }

		public string AuthorName { get; set; }

		public int Version { get; set; }

		public string Topic { get; set; }

		public string Slug { get; set; }

		public Instant Published { get; set; }

		public string Content { get; set; }

		public static ArticleHistory FromArticle(Article article)
		{

			return new ArticleHistory
			{
				Article = article,
				AuthorId = article.AuthorId,
				AuthorName = article.AuthorName,
				Content = article.Content,
				Published = article.Published,
				Slug = article.Slug,
				Topic = article.Topic,
				Version = article.Version
			};

		}

	}


}
