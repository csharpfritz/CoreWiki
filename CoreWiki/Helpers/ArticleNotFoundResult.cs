using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreWiki.Helpers
{
	public class ArticleNotFoundResult : ViewResult
	{
		private static TelemetryClient telemetry = new TelemetryClient();
		public ArticleNotFoundResult(string name = "")
		{

			if (!string.IsNullOrEmpty(name))		// do not track if no name was submitted
			{
				var documentDictionary = new Dictionary<string, string>
						{
								{"Document", name }
						};

				telemetry.TrackEvent("Missing Document", documentDictionary);

			}
			ViewName = "ArticleNotFound";
			StatusCode = 404;
		}
	}
}
