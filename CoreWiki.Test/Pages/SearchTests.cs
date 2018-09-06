using CoreWiki.Application.Articles.Search;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Application.Articles.Search.Queries;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using CoreWiki.Pages;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CoreWiki.Test.Pages
{
	public class SearchTests
	{
		[Fact]
		public async Task OnGetAsync_WithPageNumberEqualsTo2And12Posts_ShouldReturnCurrentPageEqualsTo2()
		{

			var mediator = new Mock<IMediator>();

			mediator.Setup(o => o.Send(It.IsAny<SearchArticlesQuery>(), default(CancellationToken))).Returns(
				Task.FromResult(new SearchResult<ArticleSearchDto>
				{
					CurrentPage = 2,
					Results = new List<ArticleSearchDto>
					{
						new ArticleSearchDto { Slug = "test11" },
						new ArticleSearchDto { Slug = "test12" }
					}
				}));

			// Act
			var searchModel = new SearchModel(mediator.Object);

			var result = await searchModel.OnGetAsync(query: "test", pageNumber: 2);

			Assert.Equal(2, searchModel.SearchResult.CurrentPage);
		}
	}
}
