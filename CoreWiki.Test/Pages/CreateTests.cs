using System.Threading;
using System.Threading.Tasks;
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
		private readonly FakeMediator _mockMediatr;
		private readonly Mock<IArticleRepository> _articleRepo;
		private readonly Mock<ILoggerFactory> _loggerFactory;
		private CreateModel _sut;

		public CreateTests()
		{
			_articleRepo = new Mock<IArticleRepository>();
			var clock = new Mock<IClock>();
			_loggerFactory = new Mock<ILoggerFactory>();

			_articleRepo.Setup(o => o.GetArticleBySlug(_existingArticleSlug)).Returns(Task.FromResult(GetExistingArticle()));

		}

		private Article GetExistingArticle()
		{
			return new Article
			{
				Slug = _existingArticleSlug
			};
		}

		[Fact]
		public async Task OnGetAsync_WithEmptyOrNullSlug_ShouldReturnPageResultWithNullArticle()
		{

			var fakeMediator = new FakeMediator(null);

			_sut = new CreateModel(fakeMediator, _articleRepo.Object, _loggerFactory.Object);

			Assert.IsType<PageResult>(await _sut.OnGetAsync(null));
			Assert.Null(_sut.Article);

			Assert.IsType<PageResult>(await _sut.OnGetAsync(""));
			Assert.Null(_sut.Article);
		}

		[Fact]
		public async Task OnGetAsync_WithExistingSlug_ShouldRedirectToArticleEditPage()
		{

			var fakeMediator = new FakeMediator(GetExistingArticle());
			_sut = new CreateModel(fakeMediator, _articleRepo.Object, _loggerFactory.Object);

			var result = await _sut.OnGetAsync(_existingArticleSlug);
			Assert.IsType<RedirectResult>(result);
			Assert.Equal($"/{_existingArticleSlug}/Edit", (result as RedirectResult).Url);
		}

		[Fact]
		public async Task OnGetAsync_WithNewSlug_ShouldReturnPageResultWithArticleThatHasTopicAndNullContent()
		{

			var fakeMediator = new FakeMediator(null);
			_sut = new CreateModel(fakeMediator, _articleRepo.Object, _loggerFactory.Object);

			var result = await _sut.OnGetAsync(_newArticleSlug);
			Assert.IsType<PageResult>(result);
			Assert.Equal(_newArticleTopic, _sut.Article.Topic);
			Assert.Null(_sut.Article.Content);
		}
	}


}
