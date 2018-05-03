using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreWiki.Helpers
{
    public class UrlHelpers
    {

        private static readonly Regex reSlugCharacters = new Regex(@"([\s,.//\\-_=])+");

        public static string URLFriendly(string title) {

            if (string.IsNullOrEmpty(title)) return "";

            var newTitle = title.ToLowerInvariant();

            newTitle = reSlugCharacters.Replace(newTitle, "-");

            return RemoveDiacritics(newTitle);

        }

    static string RemoveDiacritics(string text)
    {
      var normalizedString = text.Normalize(NormalizationForm.FormD);
      var stringBuilder = new StringBuilder();

      foreach (var c in normalizedString)
      {
        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        {
          stringBuilder.Append(c);
        }
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

  }
}
