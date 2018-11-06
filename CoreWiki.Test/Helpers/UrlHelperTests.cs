using CoreWiki.Application.Common;
using Xunit;

namespace CoreWiki.Test.Helpers
{
	public class UrlHelperTests
	{
		[Theory]
		[InlineData(null, "")]
		[InlineData("", "")]
		[InlineData("one-two", "One Two")]
		[InlineData("home-page", "Home Page")]
		[InlineData("onetwo", "Onetwo")]
		[InlineData("one-two-three", "One Two Three")]
		[InlineData("él-sofá", "Él Sofá")]
		public void SlugShouldBeATopic(string slug, string expected_topic)
		{
			var actual_topic = UrlHelpers.SlugToTopic(slug);
			Assert.Equal(expected_topic, actual_topic);
		}
	}
}
