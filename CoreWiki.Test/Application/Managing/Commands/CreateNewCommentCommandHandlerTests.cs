using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Reading;
using CoreWiki.Application.Articles.Reading.Commands;
using CoreWiki.Application.Articles.Reading.Dto;
using Moq;
using Xunit;

namespace CoreWiki.Test.Application.Managing.Commands
{
	public class CreateNewCommentCommandHandlerTests
	{
		private readonly CreateNewCommentCommandHandler _createNewCommentCommandHandler;
		private readonly IArticleReadingService _articleReadingService;
		private readonly IMapper _mapper;
		private readonly CreateNewCommentCommand _createNewCommentCommand;
		private readonly CreateCommentDto _createCommentDto;

		public CreateNewCommentCommandHandlerTests()
		{
			_articleReadingService = Mock.Of<IArticleReadingService>();
			_mapper = Mock.Of<IMapper>();
			_createNewCommentCommandHandler = new CreateNewCommentCommandHandler(_articleReadingService, _mapper);

			_createNewCommentCommand = new CreateNewCommentCommand();
			_createCommentDto = new CreateCommentDto();
			Mock.Get(_mapper).Setup(m => m.Map<CreateCommentDto>(_createNewCommentCommand)).Returns(_createCommentDto);

		}

		[Fact]
		public async Task Handle_HappyPath_SuccessfulResult()
		{
			var result = await _createNewCommentCommandHandler.Handle(_createNewCommentCommand, CancellationToken.None);

			Mock.Get(_articleReadingService).Verify(s => s.CreateComment(_createCommentDto));
			Assert.True(result.Successful);
		}

		[Fact]
		public async Task Handle_ArticleReadingServiceThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();
			Mock.Get(_articleReadingService).Setup(s => s.CreateComment(_createCommentDto)).Throws(exception);

			var result = await _createNewCommentCommandHandler.Handle(_createNewCommentCommand, CancellationToken.None);

			Assert.False(result.Successful);
			Assert.Same(exception, result.Exception.InnerException);
			Assert.Matches("There was an error creating the comment", result.Exception.Message);
		}

		[Fact]
		public async Task Handle_MappingThrows_UnsuccessfulWithException()
		{
			var exception = new Exception();
			Mock.Get(_mapper).Setup(m => m.Map<CreateCommentDto>(_createNewCommentCommand)).Throws(exception);

			var result = await _createNewCommentCommandHandler.Handle(_createNewCommentCommand, CancellationToken.None);

			Assert.False(result.Successful);
			Assert.Same(exception, result.Exception.InnerException);
			Assert.Matches("There was an error creating the comment", result.Exception.Message);
		}

	}
}
