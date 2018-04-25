using System;
using System.Collections.Generic;
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
        [Required , MaxLength(100)]
		public string Topic { get; set; }

		[NotMapped]
		public Instant Published { get; set; }

		// Buddy property (?)
		[Obsolete("This property only exists for EF-serialization purposes")]
		[DataType(DataType.DateTime)]
		[Column("Published")]
		public DateTime PublishedDateTime
		{
			get => Published.ToDateTimeUtc();
			// TODO: Remove this ugly hack
			set => Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
		}

		[DataType(DataType.MultilineText)]
		public string Content { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Article()
        {
            this.Comments = new HashSet<Comment>();
        }
    }
}
