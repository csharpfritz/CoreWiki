using CoreWiki.Application.Articles.Queries;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Pages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CoreWiki.Test.Pages.Create
{
	public class OnGet : BaseFixture
	{

		protected const string _existingArticleSlug = "home-page";

		public OnGet() : base()
		{

			_articleRepo.Setup(o => o.GetArticleBySlug(_existingArticleSlug)).Returns(Task.FromResult(GetExistingArticle()));
			_sut = new CreateModel(_mediator.Object, _articleRepo.Object, new NullLoggerFactory());

		}
		protected Article GetExistingArticle() => new Article { Slug = _existingArticleSlug };

		[Fact]
		public async Task WithEmptyOrNullSlug_ShouldReturnPageResultWithNullArticle()
		{

			Assert.IsType<PageResult>(await _sut.OnGetAsync(null));
			Assert.Null(_sut.Article);

			Assert.IsType<PageResult>(await _sut.OnGetAsync(""));
			Assert.Null(_sut.Article);
		}

		[Fact]
		public async Task WithExistingSlug_ShouldRedirectToArticleEditPage()
		{

			_mediator.Setup(o => o.Send(It.IsAny<GetArticle>(), default(CancellationToken))).ReturnsAsync(GetExistingArticle());

			var result = await _sut.OnGetAsync(_existingArticleSlug);
			Assert.IsType<RedirectResult>(result);
			Assert.Equal($"/{_existingArticleSlug}/Edit", (result as RedirectResult).Url);
		}

		[Fact]
		public async Task WithNewSlug_ShouldReturnPageResultWithArticleThatHasTopicAndNullContent()
		{

			var result = await _sut.OnGetAsync(_newArticleSlug);
			Assert.IsType<PageResult>(result);
			Assert.Equal(_newArticleTopic, _sut.Article.Topic);
			Assert.Null(_sut.Article.Content);
		}


	}
}
