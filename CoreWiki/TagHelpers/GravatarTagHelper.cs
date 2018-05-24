using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreWiki.TagHelpers
{
  [HtmlTargetElement("img", Attributes = "gravatar-email")]
  public class GravatarTagHelper : TagHelper
  {
		[HtmlAttributeName("gravatar-email")]
		public string Email { get; set; }
		[HtmlAttributeName("gravatar-mode")]
		public Mode Mode { get; set; } = Mode.Mm;
		[HtmlAttributeName("gravatar-rating")]
		public Rating Rating { get; set; } = Rating.g;
		[HtmlAttributeName("gravatar-size")]
		public int Size { get; set; } = 50;
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var hash = HashEmailForGravatar(Email);
			var url = $"//gravatar.com/avatar/{hash}";
			var queryBuilder = new QueryBuilder();
			queryBuilder.Add("s", Size.ToString());
			queryBuilder.Add("d", GetModeValue(Mode));
			queryBuilder.Add("r", Rating.ToString());
			url = url + queryBuilder.ToQueryString();
			output.Attributes.SetAttribute("src", url);
		}

		private static string GetModeValue(Mode mode)
		{
			if (mode == Mode.NotFound)
			{
			return "pg";
			}

			return mode.ToString().ToLower();
		}
		public static string HashEmailForGravatar(string email)
		{
			var md5Hasher = MD5.Create();
			var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
			var sBuilder = new StringBuilder();
			for (var i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}
  }

  public enum Rating
  {
		g,
		pg,
		r,
		x
  }

  public enum Mode
  {
		[Display(Name = "404")]
		NotFound,
		[Display(Name = "Mm")]
		Mm,
		[Display(Name = "Identicon")]
		Identicon,
		[Display(Name = "Monsterid")]
		Monsterid,
		[Display(Name = "Wavatar")]
		Wavatar,
		[Display(Name = "Retro")]
		Retro,
		[Display(Name = "Blank")]
		Blank
  }
}
