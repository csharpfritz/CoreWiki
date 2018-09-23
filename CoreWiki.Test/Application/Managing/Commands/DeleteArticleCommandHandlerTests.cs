using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing;
using CoreWiki.Application.Articles.Managing.Commands;
using Moq;
using Xunit;

namespace CoreWiki.Test.Application.Managing.Commands
{
	public class DeleteArticleCommandHandlerTests
	{
		private readonly DeleteArticleCommandHandler _deleteArticleCommandHandler;
		private readonly IArticleManagementService _articleManagementService;
		private readonly DeleteArticleCommand _deleteArticleCommand;

		private const string SomeSlug = "some slug";

		public DeleteArticleCommandHandlerTests()
		{
			_articleManagementService = Mock.Of<IArticleManagementService>();
			_deleteArticleCommandHandler = new DeleteArticleCommandHandler(_articleManagementService);

			_deleteArticleCommand = new DeleteArticleCommand(SomeSlug);
		}

		[Fact]
		public async Task Handle_HappyPath_Successful()
		{
			var result = await _deleteArticleCommandHandler.Handle(_deleteArticleCommand, CancellationToken.None);

			Mock.Get(_articleManagementService).Verify(s => s.Delete(SomeSlug));
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task Handle_ArticleManagementServiceThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();
			Mock.Get(_articleManagementService).Setup(s => s.Delete(SomeSlug)).Throws(exception);

			var result = await _deleteArticleCommandHandler.Handle(_deleteArticleCommand, CancellationToken.None);

			Assert.False(result.Successful);
			Assert.Matches("There was an error deleting the article", result.Exception.Message);
			Assert.Same(exception, result.Exception.InnerException);
		}
	}
}
