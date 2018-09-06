﻿using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Application.Articles.Managing.Queries;
using CoreWiki.Application.Common;
using CoreWiki.Pages;
using CoreWiki.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace CoreWiki.Test.Website.Pages.Create
{
	public class OnPost : BaseFixture
	{

		private const string _expectedSlug = "fake-topic";
		private const string _topic = "fake topic";
		private const string _content = "some content";

		[Fact]
		public async Task ShouldCreateNewNonExistingArticleAndRedirect_GivenUserIsAuthenticated()
		{
			var expectedCommand = new CreateNewArticleCommand
			{
				Topic = _topic,
				Slug =  _expectedSlug,
				AuthorId = userId,
				Content = _content,
				AuthorName = username
			};

			_mediator.Setup(mediator => mediator.Send(It.IsAny<CreateNewArticleCommand>(), It.IsAny<CancellationToken>()))
				.Returns(() => Task.FromResult(new CommandResult {Successful=true }));

			_mediator.Setup(mediator => mediator.Send(It.IsAny<GetIsTopicAvailableQuery>(), It.IsAny<CancellationToken>()))
				.Returns(() => Task.FromResult(false));

			_sut = new CreateModel(_mediator.Object, _mapper, new NullLoggerFactory())
			{
				Article = new ArticleCreate
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
			_mediator.Verify(m => m.Send(
				It.Is<GetIsTopicAvailableQuery>(request =>
					request.ArticleId.Equals(0) &&
					request.Slug.Equals(_expectedSlug)),
				It.Is<CancellationToken>(token => token.Equals(CancellationToken.None))), Times.Once
				);
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
