using CoreWiki.Application.Helpers;
using Xunit;

namespace CoreWiki.Test.Helpers
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData(" 12 34", 2)]
        [InlineData("Test", 1)]
        [InlineData(" Test", 1)]
        [InlineData(" Test ", 1)]
        [InlineData("  Test double space  ", 3)]
        [InlineData("Test double  space", 3)]
        [InlineData("Don't count \" spaced quotes \"", 4)]
        [InlineData("test content fair dinkum 12345", 5)]
        public void WordCountShouldBeAccurate(string sentence, int expected_word_count)
        {
            var actual_word_count = sentence.WordCount();
            Assert.Equal(expected_word_count, actual_word_count);
        }
    }
}
