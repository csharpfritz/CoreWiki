using System;
using System.Globalization;

namespace CoreWiki.Helpers
{
	public static class StringHelpers
	{
		/// <summary>
		/// 	Returns the singular or plural noun based on the value of the count argument
		/// </summary>
		/// <param name="singular">E.g person</param>
		/// <param name="plural">E.g people</param>
		/// <param name="count">The total number of items</param>
		/// <param name="prependCount">true returns "4 people", false returns just "people"</param>
		/// <returns></returns>
		public static string Pluralize(string singular, string plural, int count, bool prependCount = false)
		{
			var noun = count == 1 ? singular : plural;
			return prependCount ? $"{count} {noun}" : noun;
		}
		// <summary>
		/// Returns the number of words in a string
		/// </summary>
		/// <param name="content">The string we wish to count the number of words contained within.</param>
		/// <returns>The number of words in the sentence</returns>
		public static int WordCount(this string content)
		{
			if (content == null) return 0;

			var wordCount = 0;
			var letterCount = 0;

			foreach (var c in content)
			{
				if (c == '\'') continue;

				if (Char.IsLetterOrDigit(c))
				{
					letterCount++;
				}
				else if (letterCount > 0)
				{
					letterCount = 0;
					wordCount++;
				}
			}

			if (letterCount > 0) wordCount++;

			return wordCount;
		}

		/// <summary>
		/// Returns the amount of time to read a string
		/// </summary>
		/// <param name="content">The string we wish to calculate.</param>
		/// <returns>A TimeSpan of time required to read the string</returns>
		public static TimeSpan CalculateReadTime(this string content)
		{
			const decimal wpm = 275.0m;
			var wordCount = content.WordCount();
			var minutes = (double)(wordCount / wpm);
			return TimeSpan.FromMinutes(minutes);
		}
		/// <summary>
		/// Returns the string supplied in title case
		/// </summary>
		/// <param name="content">The string we wish to change to title case.</param>
		/// <param name="culture">The current user's culture, defaulting to en-US.</param>
		/// <returns>A string in title case according to the supplied culture.</returns>
		public static string ToTitleCase(this string content, string culture = "en-US")
		{
			var textInfo = new CultureInfo(culture, false).TextInfo;
			return textInfo.ToTitleCase(content);
		}
		/// <summary>
		/// Returns the string supplied in title case
		/// </summary>
		/// <param name="content">The string we wish to change to title case.</param>
		/// <param name="culture">The current user's culture, defaulting to en-US.</param>
		/// <returns>A string in title case according to the supplied culture.</returns>
		public static string RemoveHyphens(this string content)
		{
			return content.Replace("-", " ");
		}
	}
}
