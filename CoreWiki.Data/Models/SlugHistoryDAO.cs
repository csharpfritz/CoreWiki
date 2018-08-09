using NodaTime;
using NodaTime.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWiki.Data.Models
{

	[Table("SlugHistories")]
	public class SlugHistoryDAO
	{
		[Required, Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public virtual ArticleDAO Article { get; set; }

		public string OldSlug { get; set; }

		[NotMapped]
		public Instant Added { get; set; }

		[Obsolete("This property only exists for EF serialization purposes")]
		[DataType(DataType.DateTime)]
		[Column("Added")]
		[EditorBrowsable(EditorBrowsableState.Never)] // Make it harder to shoot are selfs in the foot.
		public DateTime AddedDateTime
		{
			get => Added.ToDateTimeUtc();
			set => Added = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
		}

		public Core.Domain.SlugHistory ToDomain() {

			return new Core.Domain.SlugHistory
			{

				Added = this.Added,
				Article = this.Article.ToDomain(),
				Id = this.Id,
				OldSlug = this.OldSlug

			};

		}

	}
}
