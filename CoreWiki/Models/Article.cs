using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Models
{
	public class Article
    {

        [Required, Key]
        public string Topic { get; set; }

		public DateTime Published { get; set; } = DateTime.UtcNow;

		[DataType(DataType.MultilineText)]
        public string Content { get; set; }

    }
}
