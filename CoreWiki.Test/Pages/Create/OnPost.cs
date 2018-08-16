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

namespace CoreWiki.Test.Pages.Create
{
	public class OnPost : BaseFixture
	{

		private const string _expectedSlug = "fake-topic";
		private const string _topic = "fake topic";
		private const string _content = "some content";

		[Fact]
		public async Task ShouldCreateNewNonExistingArticleAndRedirect_GivenUserIsAuthenticated()
		{
			var expectedCommand = new CreateNewArticleCommand(
				topic: _topic,
				slug: _expectedSlug,
				authorId: userId,
				content: _content,
				authorName: username);

			_articleRepo.Setup(repo => repo.IsTopicAvailable(_expectedSlug, It.IsAny<int>()))
				.Returns(() => Task.FromResult(false));
			_mediator.Setup(mediator => mediator.Send(It.IsAny<CreateNewArticleCommand>(), It.IsAny<CancellationToken>()))
				.Returns(() => Task.FromResult(new CommandResult {Successful=true }));

			_sut = new CreateModel(_mediator.Object, _articleRepo.Object, new NullLoggerFactory())
			{
				Article = new ArticleCreateDTO
				{
					Topic = _topic,
					Content = _content
				}
			};

			//we depend on a valid ClaimsPrinciple!! No check on User yet before sending the command
			_sut.AddPageContext(username, userId); 
			var result = await _sut.OnPostAsync();

			Assert.IsType<RedirectResult>(result);
			Assert.Equal($"/wiki/{_expectedSlug}", ((RedirectResult)result).Url);
			_articleRepo.Verify(repository => repository.IsTopicAvailable(It.Is<string>(slug => slug.Equals(_expectedSlug)), It.IsAny<int>()), Times.Once);
			_mediator.Verify(m => m.Send(
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
