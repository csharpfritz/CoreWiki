using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Pages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using NodaTime;
using Xunit;

namespace CoreWiki.Test.Pages
{
	public class CreateTests
	{
		private const string _existingArticleSlug = "home-page";
		private const string _newArticleSlug = "new-page";
		private const string _newArticleTopic = "New Page";
		private readonly Mock<IMediator> _mediator;
		private CreateModel _sut;

		private Article GetExistingArticle() => new Article { Slug = _existingArticleSlug };

		public CreateTests()
		{
			var articleRepo = new Mock<IArticleRepository>();
			var loggerFactory = new Mock<ILoggerFactory>();
			_mediator = new Mock<IMediator>();

			articleRepo.Setup(o => o.GetArticleBySlug(_existingArticleSlug)).Returns(Task.FromResult(GetExistingArticle()));
			_sut = new CreateModel(_mediator.Object, articleRepo.Object, loggerFactory.Object);
		}

		[Fact]
		public async Task OnGetAsync_WithEmptyOrNullSlug_ShouldReturnPageResultWithNullArticle()
		{
			Assert.IsType<PageResult>(await _sut.OnGetAsync(null));
			Assert.Null(_sut.Article);

			Assert.IsType<PageResult>(await _sut.OnGetAsync(""));
			Assert.Null(_sut.Article);
		}

		[Fact]
		public async Task OnGetAsync_WithExistingSlug_ShouldRedirectToArticleEditPage()
		{
			_mediator.Setup(o => o.Send(It.IsAny<GetArticle>(), default(CancellationToken))).ReturnsAsync(GetExistingArticle());
			var result = await _sut.OnGetAsync(_existingArticleSlug);
			Assert.IsType<RedirectResult>(result);
			Assert.Equal($"/{_existingArticleSlug}/Edit", (result as RedirectResult).Url);
		}

		[Fact]
		public async Task OnGetAsync_WithNewSlug_ShouldReturnPageResultWithArticleThatHasTopicAndNullContent()
		{
			var result = await _sut.OnGetAsync(_newArticleSlug);
			Assert.IsType<PageResult>(result);
			Assert.Equal(_newArticleTopic, _sut.Article.Topic);
			Assert.Null(_sut.Article.Content);
		}
	}
}
