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
	}
}
