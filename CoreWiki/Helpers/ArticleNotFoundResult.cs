using Microsoft.AspNetCore.Mvc;

namespace CoreWiki.Helpers
{
	public class ArticleNotFoundResult : ViewResult
	{
		public ArticleNotFoundResult()
		{
			ViewName = "ArticleNotFound";
			StatusCode = 404;
		}
	}
}