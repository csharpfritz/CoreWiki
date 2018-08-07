using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CoreWiki.Data.Models
{
	public class Article
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		// public Article(string topic, Author authorName )
		// {
		// 	Topic=topic;
		// 	AuthorId=Guid.NewGuid
		// }

		//[Required, MaxLength(100)]
		//[Display(Name = "Topic")]
		public string Topic { get; set; }

		public string Slug { get; set; }

		//[Required]
		public int Version { get; set; } = 1;

		[NotMapped]
		public Instant Published { get; set; }

		//[Required]
		public Guid AuthorId { get; set; } = Guid.NewGuid();

		//[Required]
		public string AuthorName { get; set; } = "Unknown";

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
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<ArticleHistory> History { get; set; }
		public Article()
		{
			this.Comments = new HashSet<Comment>();
			this.History = new HashSet<ArticleHistory>();
		}

		public int ViewCount { get; set; } = 0;

		public static Article FromDomain(Core.Domain.Article article) {

			return new Article
			{

				AuthorId = article.AuthorId,
				AuthorName = article.AuthorName,
				Comments = article.Comments.Select(c => Comment.FromDomain(c)).ToHashSet(),
				Content = article.Content,
				History = article.History.Select(h => ArticleHistory.FromDomain(h)).ToHashSet(),
				Id = article.Id,
				Published = article.Published,
				Slug = article.Slug,
				Topic = article.Topic,
				Version = article.Version,
				ViewCount = article.ViewCount

			};

		}

		public Core.Domain.Article ToDomain() {

			return new Core.Domain.Article
			{

				AuthorId = AuthorId,
				AuthorName = AuthorName,
				Comments = Comments.Select(c => c.ToDomain()).ToHashSet(),
				Content = Content,
				History = History.Select(h => h.ToDomain()).ToHashSet(),
				Id = Id,
				Published = Published,
				Slug = Slug,
				Topic = Topic,
				Version = Version,
				ViewCount = ViewCount
			};

		}

	}

}
