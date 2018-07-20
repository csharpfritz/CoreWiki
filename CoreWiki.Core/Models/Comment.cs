using NodaTime;
using NodaTime.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWiki.Core.Models
{
	public class Comment
	{
		[Required, Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public int IdArticle { get; set; }

		public virtual Article Article { get; set; }

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
	}
}
