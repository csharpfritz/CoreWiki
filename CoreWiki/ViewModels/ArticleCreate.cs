using System.ComponentModel.DataAnnotations;

namespace CoreWiki.ViewModels
{
	public class ArticleCreate
	{

		[Required]
		public string Topic { get; set; }

		public string Content { get; set; }
	}
}

