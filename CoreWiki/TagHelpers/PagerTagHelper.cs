using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.TagHelpers
{

	/// <summary>
	/// <see cref="ITagHelper"/> implementation targeting &lt;pager&gt; elements.
	/// </summary>
	/// <remarks>
	/// Generates a bootstrap 4 pagination block.
	/// </remarks>
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
					TagBuilder a;
					a = _Generator.GeneratePageLink(
						ViewContext,
						linkText: pageNum.ToString(),
						pageName: AspPage,
						pageHandler: string.Empty,
						protocol: string.Empty,
						hostname: string.Empty,
						fragment: string.Empty,
						routeValues: MakeRouteValues(pageNum),
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

		private Dictionary<string, string> MakeRouteValues(int pageNumber)
		{
			var route = new Dictionary<string, string>
			{
				{"PageNumber", pageNumber.ToString()}
			};

			if (UrlParams == null)
			{
				return route;
			}

			foreach (var key in UrlParams.Keys)
			{
				// We don't want to override existing values such as PageNumber
				if (route.ContainsKey(key))
				{
					continue;
				}
				route.Add(key, UrlParams[key]);
			}

			return route;
		}

		/// <summary>
		/// The name of the page.
		/// </summary>
		/// <remarks>
		/// Can be <c>null</c> if refering to the current page.
		/// </remarks>
		public string AspPage { get; set; }

		/// <summary>
		/// 	Optional. Enables adding url parameters (e.g '?query=test') to the link URL
		/// </summary>
		public Dictionary<string, string> UrlParams { get; set; }

		/// <summary>
		/// The number of the current page.
		/// </summary>
		/// <remarks>
		/// If not specified this will default to <c>1</c>.
		/// </remarks>
		public int CurrentPage { get; set; } = 1;

		/// <summary>
		/// The number of page links to show
		/// </summary>
		/// <remarks>
		/// This is required and can not be <c>null</c>.
		/// </remarks>
		public int TotalPages { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Rendering.ViewContext"/> for the current request.
		/// </summary>
		[HtmlAttributeNotBound]
		[ViewContext]
		public ViewContext ViewContext { get; set; }
	}
}
