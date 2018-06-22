using System;
using System.Text.RegularExpressions;

namespace CoreWiki.Helpers
{
	public static class StringHelpers
	{
		// <summary>
		/// Returns the number of words in a string
		/// </summary>
		/// <param name="content">The string we wish to count the number of words contained within.</param>
		/// <returns>The number of words in the sentance</returns>
		public static int WordCount(this string content)
		{
			var wordCount = 0;
			for (var i = 1; i < content.Length; i++)
			{
				if (char.IsWhiteSpace(content[i]) || i == content.Length)
				{
					if (!char.IsWhiteSpace(content[i - 1]))
						wordCount += 1;
				}
			}
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
	}
}
