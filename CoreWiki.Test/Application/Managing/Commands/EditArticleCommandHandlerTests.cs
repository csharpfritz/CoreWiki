using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Core.Domain;
using Moq;
using Xunit;

namespace CoreWiki.Test.Application.Managing.Commands
{
	public class EditArticleCommandHandlerTests
	{
		private readonly EditArticleCommandHandler _articleCommandHandler;
		private readonly IArticleManagementService _articleManagementService;
		private readonly EditArticleCommand _editArticleCommand;

		public EditArticleCommandHandlerTests()
		{
			_articleManagementService = Mock.Of<IArticleManagementService>();
			_articleCommandHandler = new EditArticleCommandHandler(_articleManagementService);

			_editArticleCommand = new EditArticleCommand
			{
				AuthorId = Guid.NewGuid(),
				Content = "Some content",
				Id = 123,
				Topic = "Some topic"
			};
		}

		[Fact]
		public async Task Handle_HappyPath_Successful()
		{
			Mock.Get(_articleManagementService)
				.Setup(s => s.Update(_editArticleCommand.Id,
					_editArticleCommand.Topic,
					_editArticleCommand.Content,
					_editArticleCommand.AuthorId))
				.ReturnsAsync(new Article { Topic = "Some topic" })
				.Verifiable();

			var result = await _articleCommandHandler.Handle(_editArticleCommand, CancellationToken.None);

			Mock.Get(_articleManagementService).Verify();
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task Handle_ArticleManagementServiceThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();
			Mock.Get(_articleManagementService).Setup(s => s.Update(_editArticleCommand.Id, _editArticleCommand.Topic, _editArticleCommand.Content, _editArticleCommand.AuthorId))
				.Throws(exception);

			var result = await _articleCommandHandler.Handle(_editArticleCommand, CancellationToken.None);

			Assert.False(result.Successful);
			Assert.Same(exception, result.Exception);
		}
	}
}
