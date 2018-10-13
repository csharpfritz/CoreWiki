using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using NodaTime;

namespace CoreWiki.Core.Domain
{
	public class Article : BaseArticle
	{
		public new string Topic
		{
			get => _Topic;
			set {
				Slug = URLFriendly(value);
				_Topic = value;
			}
		}

		private string _Topic;

		public int Version { get; set; } = 1;

		public Instant Published { get; set; }

		public Guid AuthorId { get; set; }

		public string AuthorName { get; set; } = "Unknown";

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<ArticleHistory> History { get; set; }

		public Article()
		{
			Comments = new HashSet<Comment>();
			History = new HashSet<ArticleHistory>();
		}

		public int ViewCount { get; set; } = 0;

		public static string HomePageSlug = "home-page";

		private static readonly Regex reSlugCharactersToBeDashes = new Regex(@"([\s,.//\\-_=])+");
		private static readonly Regex reSlugCharactersToRemove = new Regex(@"([^0-9a-z\-])+");
		private static readonly Regex reSlugDashes = new Regex(@"([\-])+");

		private static readonly Regex reSlugCharacters = new Regex(@"([\s,.//\\-_=])+");

		public string URLFriendly(string title)
		{

			if (string.IsNullOrEmpty(title)) return "";


			var newTitle = RemoveDiacritics(title);

			newTitle = Regex.Replace(newTitle, "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", @"-$1");

			newTitle = reSlugCharactersToBeDashes.Replace(newTitle, "-");

			newTitle = newTitle.ToLowerInvariant();

			newTitle = reSlugCharactersToRemove.Replace(newTitle, "");

			newTitle = reSlugDashes.Replace(newTitle, "-");

			newTitle = newTitle.Trim('-');

			return newTitle;

		}

		private static string RemoveDiacritics(string text)
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
