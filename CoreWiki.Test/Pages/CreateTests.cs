using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Pages;
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
		private readonly CreateModel _sut;

		public CreateTests()
		{
			var articleRepo = new Mock<IArticleRepository>();
			var clock = new Mock<IClock>();
			var loggerFactory = new Mock<ILoggerFactory>();

			articleRepo.Setup(o => o.GetArticleBySlug(_existingArticleSlug)).Returns(GetExistingArticle);

			_sut = new CreateModel(articleRepo.Object, clock.Object, loggerFactory.Object);
		}

		private async Task<Article> GetExistingArticle()
		{
			return new Article
			{
				Slug = _existingArticleSlug
			};
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
