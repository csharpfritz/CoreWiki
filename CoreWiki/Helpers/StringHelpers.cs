namespace CoreWiki.Helpers
{
	public static class StringHelpers
	{
		/// <summary>
		/// 	Returns the singular or plural noun based on the value of the count argument
		/// </summary>
		/// <param name="singular">E.g apple</param>
		/// <param name="plural">E.g apples</param>
		/// <param name="count">The total number of the item</param>
		/// <param name="prependCount">true returns "4 apples", false returns just "apples"</param>
		/// <returns></returns>
		public static string Pluralize(string singular, string plural, int count, bool prependCount = false)
		{
			var noun = count == 1 ? singular : plural;
			return prependCount ? $"{count} {noun}" : noun;
		}
	}
}
