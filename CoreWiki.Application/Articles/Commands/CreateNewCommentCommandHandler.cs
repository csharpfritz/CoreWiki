﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Services;
using CoreWiki.Application.Articles.Services.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Commands
{
	public class CreateNewCommentCommandHandler : IRequestHandler<CreateNewCommentCommand, CommandResult>
	{
		private readonly IArticleReadingService _articleReadingService;
		private readonly IMapper _mapper;

		public CreateNewCommentCommandHandler(IArticleReadingService articleReadingService, IMapper mapper)
		{
			_articleReadingService = articleReadingService;
			_mapper = mapper;
		}

		public async Task<CommandResult> Handle(CreateNewCommentCommand request, CancellationToken cancellationToken)
		{
			var result = new CommandResult() { Successful = true };

			try
			{
				var comment = _mapper.Map<CreateCommentDto>(request);
				await _articleReadingService.CreateComment(comment);
			}
			catch (Exception ex)
			{
				result.Successful = false;
				result.Exception = new CreateCommentException("There was an error creating the comment", ex);
			}

			return result;

		}
	}
}
