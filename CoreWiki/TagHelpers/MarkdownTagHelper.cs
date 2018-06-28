using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

		public string ConvertMarkdown(string markdown)
		{
			var sanitizer = new HtmlSanitizer();
			sanitizer.RemovingAttribute += OnRemovingAttribute;

			// disallow form elements
			sanitizer.AllowedTags.Remove("form");
			sanitizer.AllowedTags.Remove("button");
			sanitizer.AllowedTags.Remove("input");

			// parse the markdown to HTML
			markdown = Westwind.AspNetCore.Markdown.Markdown.Parse(markdown);

			// encode any custom tags (pasting e-mails and other markup (XML))
			// From author of HtmlSanitizer
			// https://gist.github.com/mganss/00bec2c2245c0ef86d9c82d6211def7b
			markdown = HtmlRegex.Replace(markdown, m =>
			{
				var tagName = m.Groups[1].Value;
				if (!HtmlTags.Contains(tagName))
					return "&lt;" + m.Value.Substring(1);
				return m.Value;
			});

			markdown = sanitizer.Sanitize(markdown);

			return markdown;
		}

		private void OnRemovingAttribute(object sender, RemovingAttributeEventArgs e)
		{
			if (!e.Attribute.Value.Contains("vbscript:", StringComparison.CurrentCultureIgnoreCase)
				&& !e.Attribute.Value.Contains("javascript:", StringComparison.CurrentCultureIgnoreCase)
				&& !e.Attribute.Value.Contains("data:", StringComparison.CurrentCultureIgnoreCase)
				&& !e.Attribute.Name.StartsWith("on", StringComparison.CurrentCultureIgnoreCase))
			{
				// don't remove the attribute if we've deemed it safe
				e.Cancel = true;
			}
		}

		// From author of HtmlSanitizer
		// https://gist.github.com/mganss/00bec2c2245c0ef86d9c82d6211def7b
		static Regex HtmlRegex = new Regex(@"</?([a-z]+[1-6]?)", RegexOptions.IgnoreCase);
		static HashSet<string> HtmlTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "a", "abbr", "acronym", "address", "applet", "area", "article", "aside", "audio", "b", "base", "bdi", "bdo", "big", "blockquote", "body", "br", "button", "canvas", "caption", "center", "cite", "code", "col", "colgroup", "command", "datalist", "dd", "del", "details", "dfn", "dir", "div", "dl", "dt", "em", "embed", "fieldset", "figcaption", "figure", "font", "footer", "form", "frame", "h1", "h2", "h3", "h4", "h5", "h6", "head", "header", "hgroup", "hr", "html", "i", "iframe", "img", "input", "ins", "isindex", "kbd", "keygen", "label", "legend", "li", "link", "map", "mark", "menu", "meta", "meter", "nav", "noscript", "object", "ol", "optgroup", "option", "output", "p", "param", "pre", "progress", "q", "rp", "rt", "ruby", "s", "samp", "script", "section", "select", "small", "source", "span", "strike", "strong", "style", "sub", "summary", "sup", "table", "tbody", "td", "textarea", "tfoot", "th", "thead", "time", "title", "tr", "track", "tt", "u", "ul", "var", "video", "wbr" };
	}
}
