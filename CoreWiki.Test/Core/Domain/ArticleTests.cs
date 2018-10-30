using CoreWiki.Core.Domain;
using Xunit;

namespace CoreWiki.Test.Core.Domain
{
	public class ArticleTests
	{
		[Theory]
		[InlineData("HomePage", "home-page")]
		public void SettingTopic_ShouldGenerateValidSlug(string topic, string expectedSlug)
		{
			// Arrange
			var sut = new Article();

			// Act
			sut.Topic = topic;

			// Assert
			Assert.Equal(expectedSlug, sut.Slug);
		}
	}
}
