using System;
using System.Text.RegularExpressions;

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
		/// <returns>The number of words in the sentance</returns>
		public static int WordCount(this string content)
		{
			if (string.IsNullOrWhiteSpace(content))
				return 0;

			var matches = Regex.Matches(content, @"\b\S+\b");
			return matches.Count;
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
	}
}
