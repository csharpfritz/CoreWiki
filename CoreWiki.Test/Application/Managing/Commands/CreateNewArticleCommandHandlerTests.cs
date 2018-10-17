using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing;
using CoreWiki.Application.Articles.Managing.Commands;
using CoreWiki.Core.Domain;
using Moq;
using Xunit;

namespace CoreWiki.Test.Application.Managing.Commands
{
	public class CreateNewArticleCommandHandlerTests
	{
		private readonly CreateNewArticleCommandHandler _createNewArticleCommandHandler;
		private readonly IMapper _mapper;
		private readonly IArticleManagementService _articleManagementService;
		private readonly CreateNewArticleCommand _createNewArticleCommand;
		private readonly Article _article;

		public CreateNewArticleCommandHandlerTests()
		{
			_mapper = Mock.Of<IMapper>();
			_article = new Article() { Id = 1 };

			var mockService = new Mock<IArticleManagementService>();
			mockService.Setup(s => s.CreateArticleAndHistory(It.IsAny<Article>())).Returns(Task.FromResult(_article)).Verifiable();
			_articleManagementService = mockService.Object;
			
			_createNewArticleCommandHandler = new CreateNewArticleCommandHandler(_articleManagementService, _mapper);
			 
			_createNewArticleCommand = new CreateNewArticleCommand();

			Mock.Get(_mapper).Setup(m => m.Map<Article>(_createNewArticleCommand)).Returns(_article);
		}

		[Fact]
		public async Task Handle_HappyPath_Successful()
		{
			var result = await _createNewArticleCommandHandler.Handle(_createNewArticleCommand, CancellationToken.None);

			Mock.Get(_articleManagementService).Verify(s => s.CreateArticleAndHistory(_article));
			Assert.True(result.Successful, result.Exception?.Message);
		}

		[Fact]
		public async Task Error_MapThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();
			Mock.Get(_mapper).Setup(s => s.Map<Article>(_createNewArticleCommand)).Throws(exception);

			var handle = await _createNewArticleCommandHandler.Handle(_createNewArticleCommand, CancellationToken.None);

			Assert.False(handle.Successful);
			Assert.Equal("There was an error creating the article", handle.Exception.Message);
			Assert.Same(handle.Exception.InnerException, exception);
		}

		[Fact]
		public async Task Error_CreateArticleAndHistoryThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();			
			Mock.Get(_articleManagementService).Setup(s => s.CreateArticleAndHistory(_article)).Throws(exception);

			var handle = await _createNewArticleCommandHandler.Handle(_createNewArticleCommand, CancellationToken.None);

			Assert.False(handle.Successful);
			Assert.Equal("There was an error creating the article", handle.Exception.Message);
			Assert.Same(exception, handle.Exception.InnerException);
		}
	}
}
