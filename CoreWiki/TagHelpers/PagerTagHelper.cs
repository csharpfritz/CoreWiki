using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreWiki.TagHelpers
{

	[HtmlTargetElement("pager")]
	public class PagerTagHelper : TagHelper
	{
		private readonly IHtmlGenerator _Generator;

		public PagerTagHelper(IHtmlGenerator generator)
		{
			_Generator = generator;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "ul";

			TagBuilder ul = new TagBuilder("ul");
			ul.AddCssClass("pagination");
			output.MergeAttributes(ul);

			for (var pageNum = 1; pageNum <= TotalPages; pageNum++)
			{
				TagBuilder li = new TagBuilder("li");
				li.AddCssClass("page-item");
				if (pageNum == CurrentPage)
				{
					li.AddCssClass("active");
					TagBuilder span1 = new TagBuilder("span");
					span1.AddCssClass("page-link");
					span1.InnerHtml.Append($"{pageNum}");
					li.InnerHtml.AppendHtml(span1);
				}
				else
				{
					object route = new { PageNumber = pageNum };
					TagBuilder a;
					a = _Generator.GeneratePageLink(
						ViewContext,
						linkText: pageNum.ToString(),
						pageName: AspPage,
						pageHandler: string.Empty,
						protocol: string.Empty,
						hostname: string.Empty,
						fragment: string.Empty,
						routeValues: route,
						htmlAttributes: null
						);
					a.AddCssClass("page-link");

					li.InnerHtml.AppendHtml(a);
				}

				output.Content.AppendHtml(li);

			}

			await base.ProcessAsync(context, output);
			return;

		}

		public string AspPage { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
		/// </summary>
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }
	}
}
