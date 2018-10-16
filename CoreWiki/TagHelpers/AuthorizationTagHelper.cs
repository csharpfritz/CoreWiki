using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace CoreWiki.TagHelpers
{
	[HtmlTargetElement("a", Attributes = "asp-policy")]
	[HtmlTargetElement("a", Attributes = "asp-policy, asp-policy-hidden")]
	[HtmlTargetElement("a", Attributes = "asp-policy, asp-policy-message")]
	[HtmlTargetElement("li", Attributes = "asp-policy, asp-policy-hidden")]
	[HtmlTargetElement("button", Attributes = "asp-policy")]
	[HtmlTargetElement("button", Attributes = "asp-policy, asp-policy-hidden")]
	[HtmlTargetElement("button", Attributes = "asp-policy, asp-policy-message")]
	[HtmlTargetElement("input", Attributes = "asp-policy")]
	[HtmlTargetElement("input", Attributes = "asp-policy, asp-policy-hidden")]
	[HtmlTargetElement("input", Attributes = "asp-policy, asp-policy-message")]
	public class AuthorizationTagHelper : TagHelper, IAuthorizeData
	{
		private readonly HttpContext _httpContext;
		private readonly IAuthorizationService _authorizationService;
		private readonly IAuthorizationPolicyProvider _policyProvider;

		public AuthorizationTagHelper(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService, IAuthorizationPolicyProvider policyProvider)
		{
			_httpContext = httpContextAccessor.HttpContext;
			_authorizationService = authorizationService;
			_policyProvider = policyProvider;
		}

		/// <summary>
		/// Gets or sets the policies that determines access to the HTML element.
		/// </summary>
		[HtmlAttributeName("asp-policy")]
		public string Policy { get; set; }

		/// <summary>
		/// Gets or sets whether the HTML block is hidden or not when the user doesn't have permission.
		/// </summary>
		[HtmlAttributeName("asp-policy-hidden")]
		public bool SuppressOutput { get; set; }

		/// <summary>
		/// Gets or sets the message placed on the HTML block when the user doesn't have permission.
		/// </summary>
		[HtmlAttributeName("asp-policy-message")]
		public string Message { get; set; }

		public string Roles { get; set; }

		public string AuthenticationSchemes { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var message = Message ?? "You do not have permission to complete this action.";
			var policy = await AuthorizationPolicy.CombineAsync(_policyProvider, new[] { this });
			var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContext.User, policy);
			if (!authorizationResult.Succeeded)
			{
				if (SuppressOutput)
				{
					// Hide the HTML element
					output.SuppressOutput();
					return;
				}

				// Add a title attribute to the HTML element explaining the reason why the element is disabled
				output.Attributes.Add("title", message);

				// Determine if a class attribute is present; if the class attribute exists then append it else add it
				if (output.Attributes.TryGetAttribute("class", out var classAttribute))
				{
					// Append the "disabled" class to the list of classes
					output.Attributes.SetAttribute("class", $"{classAttribute.Value} disabled");
				}
				else
				{
					// Add the "disabled" class to the HTML element
					output.Attributes.Add("class", "disabled");
				}

				switch (context.TagName)
				{
					// If the HTML element is a link, convert the element to a span and remove the href attribute
					case "a":
						output.Attributes.Remove(output.Attributes["href"]);
						output.TagName = "span";
						break;
					// If the HTML element is an input or submit element, disable it
					case "button":
					case "input":
						output.Attributes.Add("disabled", "");
						break;
				}
			}
		}
	}
}
