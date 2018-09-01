using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Notifications;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Commands
{
	public class CreateNewCommentCommandHandler : IRequestHandler<CreateNewCommentCommand, CommandResult>
	{
		private readonly ICommentRepository _commentRepo;
		private readonly IClock _clock;
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public CreateNewCommentCommandHandler(ICommentRepository commentRepo, IClock clock, IMapper mapper, IMediator mediator)
		{
			_commentRepo = commentRepo;
			_clock = clock;
			_mapper = mapper;
			_mediator = mediator;
		}

		public async Task<CommandResult> Handle(CreateNewCommentCommand request, CancellationToken cancellationToken)
		{
			var result = new CommandResult() { Successful = true };

			try
			{
				var comment = _mapper.Map<Comment>(request);
				comment.Submitted = _clock.GetCurrentInstant();

				await _commentRepo.CreateComment(comment);
				_mediator.Publish(new CommentPostedNotification(request.Article, request.Comment));
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
