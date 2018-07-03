using System;
using CoreWiki.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CoreWiki.Validation
{
	public class ArticleValidator : AbstractValidator<Article>
	{
		public ArticleValidator(IStringLocalizer<ArticleValidator> localizer)
		{
			RuleFor(o => o.Topic)
				.NotEmpty().WithMessage(localizer["ArticleTopicIsRequired"])
				.MaximumLength(100).WithMessage(localizer["ArticleTopicExceedsMaximumLength"]);
		}
	}
}
