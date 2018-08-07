using NodaTime;
using NodaTime.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWiki.Data.Models
{
	public class ArticleHistory
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public virtual Article Article { get; set; }

		[ForeignKey(nameof(Article))]
		public int ArticleId { get; set; }

		[Required]
		public Guid AuthorId { get; set; }

		public string AuthorName { get; set; }

		[Required]
		public int Version { get; set; }

		[Required, MaxLength(100)]
		[Display(Name = "Topic")]
		public string Topic { get; set; }

		public string Slug { get; set; }

		[NotMapped]
		public Instant Published { get; set; }

		// Buddy property (?)
		[Obsolete("This property only exists for EF-serialization purposes")]
		[DataType(DataType.DateTime)]
		[Column("Published")]
		[EditorBrowsable(EditorBrowsableState.Never)] // Make it harder to shoot are selfs in the foot.
		public DateTime PublishedDateTime
		{
			get => Published.ToDateTimeUtc();
			// TODO: Remove this ugly hack
			set => Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
		}

		[DataType(DataType.MultilineText)]
		[Display(Name = "Content")]
		public string Content { get; set; }

		public static ArticleHistory FromArticle(Article article)
		{

			return new ArticleHistory
			{
				//Id = 1,
				Article = article,
				ArticleId = article.Id,
				AuthorId = article.AuthorId,
				AuthorName = article.AuthorName,
				Content = article.Content,
				Published = article.Published,
				Slug = article.Slug,
				Topic = article.Topic,
				Version = article.Version
			};

		}

		public static ArticleHistory FromDomain(Core.Domain.ArticleHistory history) {

			return new ArticleHistory {

				ArticleId = history.Article.Id,
				AuthorId = history.AuthorId,
				AuthorName = history.AuthorName,
				Content = history.Content,
				Id = history.Id,
				Published = history.Published,
				Slug = history.Slug,
				Topic = history.Topic,
				Version = history.Version

			};

		}

		public Core.Domain.ArticleHistory ToDomain() {

			return new Core.Domain.ArticleHistory
			{

				Article = Article.ToDomain(),
				AuthorId = AuthorId,
				AuthorName = AuthorName,
				Content = Content,
				Id = Id,
				Published = Published,
				Slug = Slug,
				Topic = Topic,
				Version = Version

			};
			
		}

	}

}
