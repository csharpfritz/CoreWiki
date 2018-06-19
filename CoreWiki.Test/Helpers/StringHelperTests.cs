using System;
using CoreWiki.Helpers;
using Xunit;

namespace CoreWiki.Test
{
    public class StringHelperTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        [InlineData("Test", 1)]
        [InlineData(" Test", 1)]
        [InlineData(" Test ", 1)]
        [InlineData("Test double  space", 3)]
        [InlineData("Don't count \" spaced quotes \"", 4)]
        public void WordCountShouldBeAccurate(string sentence, int expected_word_count)
        {
            int actual_word_count = sentence.WordCount();
            Assert.Equal(expected_word_count, actual_word_count);
        }
    }
}
