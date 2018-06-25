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
