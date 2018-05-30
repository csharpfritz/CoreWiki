using Xunit;
using System;
using CoreWiki.Helpers;

namespace Test.UrlFriendly
{
    
  public class GivenPascalCase {

    private const string GIVEN = "HomePage";

    [Fact]
    public void ShouldAddDashBetweenWords() {

      // act
      var result = UrlHelpers.URLFriendly(GIVEN);

      // assert
      Assert.Equal("home-page", result);


    }

  }

}