using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Ganss.XSS;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreWiki.TagHelpers
{
	[HtmlTargetElement("markdown")]
	public class MarkdownTagHelper : TagHelper
	{
		[HtmlAttributeName("markdown")]
		public ModelExpression Markdown { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			await base.ProcessAsync(context, output);

			var markdown = Markdown?.Model?.ToString() ?? "";
			var content = ConvertMarkdown(markdown);

			output.TagName = null;
			output.Content.SetHtmlContent(content);
		}

		public string ConvertMarkdown(string unsafeMarkdown)
		{
			var encodedMarkdown = HttpUtility.HtmlEncode(unsafeMarkdown);
			var unsafeContent = Westwind.AspNetCore.Markdown.Markdown.Parse(encodedMarkdown);
			var safeContent = new HtmlSanitizer().Sanitize(unsafeContent);

			return safeContent;
		}
	}
}
