using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Extensions;

namespace CoreWiki.Models
{
	public class Article
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required, MaxLength(100)]
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
		public string Content { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public Article()
		{
			this.Comments = new HashSet<Comment>();
		}

		public int ViewCount { get; set; } = 0;

	}

}
