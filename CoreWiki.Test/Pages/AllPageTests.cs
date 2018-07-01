using CoreWiki.Models;
using CoreWiki.Pages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoreWiki.Test.Pages
{
	public class AllPageTests
	{

		[Fact]
		public async Task OnGet_PopulatesThePageModel_WithAListOfArticles()
		{

			// Arrange
			var optionsBuilder = new DbContextOptionsBuilder<InMemoryDbContext>();
			optionsBuilder.UseInMemoryDatabase("InMemoryDb");
			var dbContext = new InMemoryDbContext(optionsBuilder.Options);
			InMemoryDbContext.SeedData(dbContext);
			var expectedArticles = dbContext.Articles as IEnumerable<Article>;
			var pageModel = new AllModel(dbContext);


			// Act
			await pageModel.OnGet();


			// Assert
			var actualArticles = Assert.IsAssignableFrom<IEnumerable<Article>>(pageModel.Articles);
			Assert.Equal(
				expectedArticles.OrderBy(m => m.Id).Select(a => a.Topic),
				actualArticles.OrderBy(m => m.Id).Select(a => a.Topic));

		}
	}
}
