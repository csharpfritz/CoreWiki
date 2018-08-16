using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Models;
using CoreWiki.Pages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
		private const string username = "John Doe";
		private Guid userId = Guid.NewGuid();
		private readonly FakeMediator _mockMediatr;
		private readonly Mock<IArticleRepository> _articleRepo;
		private readonly Mock<IMediator> _mediator;
		private CreateModel _sut;

		private Article GetExistingArticle() => new Article { Slug = _existingArticleSlug };

		public CreateTests()
		{

			var articleRepo = new Mock<IArticleRepository>();
			_mediator = new Mock<IMediator>();

			articleRepo.Setup(o => o.GetArticleBySlug(_existingArticleSlug)).Returns(Task.FromResult(GetExistingArticle()));
			_sut = new CreateModel(_mediator.Object, articleRepo.Object, new NullLoggerFactory());

		}

		private PageContext MockPageContext()
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, username),
				new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
				new Claim("name", username)
			};
			var identity = new ClaimsIdentity(claims);
			var principle = new ClaimsPrincipal(identity);
			// use default context with user
			var httpContext = new DefaultHttpContext()
			{
				User = principle
			};
			//need these as well for the page context
			var modelState = new ModelStateDictionary();
			var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
			var modelMetadataProvider = new EmptyModelMetadataProvider();
			var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
			// need page context for the page model
			return new PageContext(actionContext)
			{
				ViewData = viewData
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

		[Fact]
		public async Task OnPostAsync_ShouldCreateNewNonExistingArticleAndRedirect_GivenUserIsAuthenticated()
		{
			var expectedSlug = "fake-topic";
			var topic = "fake topic";
			var content = "some content";
			var expectedCommand = new CreateNewArticleCommand(
				topic: topic,
				slug: expectedSlug,
				authorId: userId,
				content: content,
				authorName: username);

			var articleRepo = new Mock<IArticleRepository>();
			articleRepo.Setup(repo => repo.IsTopicAvailable(expectedSlug, It.IsAny<int>()))
				.Returns(() => Task.FromResult(false));
			var mediatr = new Mock<IMediator>();
			mediatr.Setup(mediator => mediator.Send(It.IsAny<CreateNewArticleCommand>(), It.IsAny<CancellationToken>()))
				.Returns(() => Task.FromResult(default(Unit)));

			_sut = new CreateModel(mediatr.Object, articleRepo.Object, new NullLoggerFactory())
			{
				PageContext = MockPageContext(), //we depend on a valid ClaimsPrinciple!! No check on User yet before sending the command
				Article = new ArticleCreateDTO
				{
					Topic = topic,
					Content = content
				}
			};
			var result = await _sut.OnPostAsync();

			Assert.IsType<RedirectResult>(result);
			Assert.Equal($"/wiki/{expectedSlug}", ((RedirectResult)result).Url);
			articleRepo.Verify(repository => repository.IsTopicAvailable(It.Is<string>(slug => slug.Equals(expectedSlug)), It.IsAny<int>()), Times.Once);
			mediatr.Verify(m => m.Send(
				It.Is<CreateNewArticleCommand>(request =>
					request.Slug.Equals(expectedCommand.Slug) &&
					request.Topic.Equals(expectedCommand.Topic) &&
					request.AuthorName.Equals(expectedCommand.AuthorName) &&
					request.Content.Equals(expectedCommand.Content) &&
					request.AuthorId.Equals(userId)),
				It.Is<CancellationToken>(token => token.Equals(CancellationToken.None))), Times.Once);
		}

	}

}
