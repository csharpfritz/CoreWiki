﻿using NodaTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using CoreWiki.Core.Common;

namespace CoreWiki.Core.Domain
{
	public class Article
	{

		public int Id { get; set; }

		public string Topic { get
			{
				return _Topic;
			}
			set {
				Slug = UrlHelpers.URLFriendly(value);
				_Topic = value;
			}
		}

		private string _Topic;

		public string Slug { get; set; }


		public int Version { get; set; } = 1;

		public Instant Published { get; set; }

		public Guid AuthorId { get; set; }

		public string AuthorName { get; set; } = "Unknown";

		public string Content { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }

		public virtual ICollection<ArticleHistory> History { get; set; }

		public Article()
		{
			this.Comments = new HashSet<Comment>();
			this.History = new HashSet<ArticleHistory>();
		}

		public int ViewCount { get; set; } = 0;

		public static string HomePageSlug = "home-page";

		private static readonly Regex reSlugCharactersToBeDashes = new Regex(@"([\s,.//\\-_=])+");
		private static readonly Regex reSlugCharactersToRemove = new Regex(@"([^0-9a-z\-])+");
		private static readonly Regex reSlugDashes = new Regex(@"([\-])+");

		private static readonly Regex reSlugCharacters = new Regex(@"([\s,.//\\-_=])+");

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
