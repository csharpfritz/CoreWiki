using System.Globalization;

namespace CoreWiki.Application.Common
{
	public class UrlHelpers
	{
		public static string SlugToTopic(string slug)
		{
			if (string.IsNullOrEmpty(slug))
			{
				return "";
			}

			var textInfo = new CultureInfo("en-US", false).TextInfo;
			var outValue = textInfo.ToTitleCase(slug);

			return outValue.Replace("-", " ");
		}
	}
}
