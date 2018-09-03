using AutoMapper;
using CoreWiki.Configuration.Startup;
using CoreWiki.Pages;
using MediatR;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Application.Articles.Reading.Queries;
using Xunit;

namespace CoreWiki.Test.Pages.Details
{
	public class OnGet
	{

		private readonly Mock<IMediator> _Mediator;
		private readonly IMapper _Mapper;

		public OnGet()
		{

			_Mediator = new Mock<IMediator>();
			_Mapper = ConfigureAutomapperServices.ConfigureAutomapper(null);

		}

		[Theory]
		[InlineData("foo", "foo")]
		public async Task ShouldMapArticleCorrectly(string slug, string title) {

			// arrange
			var rdm = new Random();
			var expectedComment = new CommentDto
			{
				AuthorId = Guid.NewGuid(),
				Content = "This is a comment... NARF",
				DisplayName = "Commenty McCommentFace",
				Email = "comments@corewiki.com",
				Id = rdm.Next(1000, 1000000)
			};
			var article = new ArticleReadingDto
				{
				Slug = slug,
				Topic = title,
				Version = rdm.Next(1,10000),
				ViewCount = rdm.Next(1,10000),
				Comments = new[] { expectedComment }
			};
			_Mediator.Setup(m => m.Send(It.IsAny<GetArticleQuery>(), default(CancellationToken))).ReturnsAsync(article);

			// act
			var sut = new DetailsModel(_Mediator.Object, _Mapper);
			sut.AddPageContext("", Guid.Empty);
			await sut.OnGetAsync(article.Slug);

			// assert
			Assert.Equal(article.Slug, sut.Article.Slug);
			Assert.Equal(article.Topic, sut.Article.Topic);
			Assert.Equal(article.ViewCount+1, sut.Article.ViewCount);
			Assert.Equal(article.Version, sut.Article.Version);
			Assert.Equal(article.Comments.Length, sut.Article.Comments.Count);

			var actualComment = sut.Article.Comments.FirstOrDefault();
			Assert.NotNull(actualComment);
			Assert.Equal(expectedComment.Content, actualComment.Content);
			Assert.Equal(expectedComment.Email, actualComment.Email);
			Assert.Equal(expectedComment.DisplayName, actualComment.DisplayName);


		}

	}
}
