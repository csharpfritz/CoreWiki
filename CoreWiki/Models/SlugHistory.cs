using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using NodaTime;
using NodaTime.Extensions;

namespace CoreWiki.Models
{
	public class SlugHistory
	{
		[Required, Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public virtual Article Article { get; set; }

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
    }
}
