using Xunit;
using FluentValidation.TestHelper;
using CoreWiki.Validation;
using Microsoft.Extensions.Localization;
using Moq;

namespace CoreWiki.Test.Validation
{
    public class ArticleValidatorTests
    {
        private ArticleValidator _validator;

        public ArticleValidatorTests()
        {
            var mock = new Mock<IStringLocalizer<ArticleValidator>>();
            mock.Setup(_ => _["ArticleTopicIsRequired"]).Returns(new LocalizedString("ArticleTopicIsRequired", "ArticleTopicIsRequired"));
            mock.Setup(_ => _["ArticleTopicExceedsMaximumLength"]).Returns(new LocalizedString("ArticleTopicExceedsMaximumLength", "ArticleTopicExceedsMaximumLength"));
            _validator = new ArticleValidator(mock.Object);
        }

        [Fact]
        public void ArticleTopic_ShouldThrowValidationError_WhenEmpty()
        {
            _validator.ShouldHaveValidationErrorFor(article => article.Topic, "");
        }

        [Fact]
        public void ArticleTopic_ShouldThrowValidationError_WhenWhitespace()
        {
            _validator.ShouldHaveValidationErrorFor(article => article.Topic, new string(' ', 10));
        }

        [Fact]
        public void ArticleTopic_ShouldThrowValidationError_WhenNull()
        {
            _validator.ShouldHaveValidationErrorFor(article => article.Topic, null as string);
        }

        [Fact]
        public void ArticleTopic_ShouldThrowValidationError_WhenLongerThanMaximumLength()
        {
            _validator.ShouldHaveValidationErrorFor(article => article.Topic, new string('a', 101));
        }

        [Fact]
        public void ArticleTopic_ShouldNotThrowValidationError_WhenWithinMaximumLength()
        {
            _validator.ShouldNotHaveValidationErrorFor(article => article.Topic, new string('a', 100));
        }
    }
}
