using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace CoreWiki.Test.SearchArticles
{

	public class SearchEngineShould
	{
		//private readonly MockRepository _Mockery;
		private readonly Mock<IArticlesSearchEngine> _MockSearchEngine;

		public SearchEngineShould()
		{

//			_Mockery = new MockRepository(MockBehavior.Loose);
			_MockSearchEngine = new Mock<IArticlesSearchEngine>();

		}

		[Fact]
		public void WhenHandlingQuery_ExecuteSearch() {

			// Arrange
			_MockSearchEngine.Setup(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
			var sut = new SearchArticlesHandler(_MockSearchEngine.Object);

			// Act
			sut.Handle(new SearchArticlesQuery("test", 1, 1), CancellationToken.None);

			// Assert
			_MockSearchEngine.Verify(m => m.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(1));

		}

	}


}
