using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Pages;
using CoreWiki.SearchEngines;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CoreWiki.Test.Pages
{
	public class SearchTests
	{
		[Fact]
		public async Task OnGetAsync_WithPageNumberEqualsTo2And12Posts_ShouldReturnCurrentPageEqualsTo2()
		{
			var articleRepo = new Mock<IArticleRepository>();
			var articleSearchEngine = new Mock<IArticlesSearchEngine>();

			articleSearchEngine.Setup(o => o.SearchAsync("test", 2, 10)).Returns(
				Task.FromResult(new SearchResult<Article>
				{
					CurrentPage = 2,
					Results = new List<Article>
					{
						new Article { Slug = "test11" },
						new Article { Slug = "test12" }
					}
				}));

			var searchModel = new SearchModel(articleSearchEngine.Object, articleRepo.Object);

			var result = await searchModel.OnGetAsync(query: "test", pageNumber: 2);

			Assert.Equal(2, searchModel.SearchResult.CurrentPage);
		}
	}
}
