using NodaTime;
using NodaTime.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWiki.Data.EntityFramework.Models
{
	[Table("Comments")]
	public class CommentDAO
	{
		[Required, Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(Article))]
		public int ArticleId { get; set; }

		// TODO: Temporary until removed from database
		[Required]
		public int IdArticle
		{
			get
			{
				return ArticleId;
			}
			set
			{
				ArticleId = value;
			}
		}

		public virtual ArticleDAO Article { get; set; }

		[Required, MaxLength(100)]
		[Display(Name = "Name")]
		public string DisplayName { get; set; }

		[Display(Name = "Email")]
		[Required, MaxLength(100), DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Not a valid Email address")]
		public string Email { get; set; }

		[NotMapped]
		public Instant Submitted { get; set; }

		[Required]
		public Guid AuthorId { get; set; } = Guid.NewGuid();

		// Buddy property (?)
		[Obsolete("This property only exists for EF-serialization purposes")]
		[DataType(DataType.DateTime)]
		[Column("Submitted")]
		public DateTime SubmittedDateTime
		{
			get => Submitted.ToDateTimeUtc();
			// TODO: Remove this ugly hack
			set => Submitted = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
		}

		[Required]
		[Display(Name = "Content")]
		[DataType(DataType.MultilineText)]
		public string Content { get; set; }

		public static CommentDAO FromDomain(Core.Domain.Comment comment)
		{

			return new CommentDAO
			{

				AuthorId = comment.AuthorId,
				Content = comment.Content,
				DisplayName = comment.DisplayName,
				Email = comment.Email,
				Id = comment.Id,
				ArticleId = comment.ArticleId,
				Submitted = comment.Submitted
			};

		}

		public Core.Domain.Comment ToDomain()
		{

			return new Core.Domain.Comment
			{

				AuthorId = AuthorId,
				Content = Content,
				DisplayName = DisplayName,
				Email = Email,
				Id = Id,
				ArticleId = this.Article.Id,
				Submitted = Submitted

			};

		}

	}
}
