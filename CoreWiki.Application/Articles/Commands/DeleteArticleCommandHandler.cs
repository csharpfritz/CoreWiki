using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Notifications;
using CoreWiki.Core.Interfaces;
using MediatR;

namespace CoreWiki.Application.Articles.Commands
{
	public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, CommandResult>
	{
		private readonly IArticleRepository _articleRepo;
		private readonly IMediator _mediator;

		public DeleteArticleCommandHandler(IArticleRepository articleRepo, IMediator mediator)
		{
			_articleRepo = articleRepo;
			_mediator = mediator;
		}

		public async Task<CommandResult> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
		{
			var result = new CommandResult() { Successful = true };

			try
			{
				var article = await _articleRepo.Delete(request.Slug);

				if (article != null)
				{
					_mediator.Publish(new ArticleDeletedNotification(article));
				}
			}
			catch (Exception ex)
			{
				result.Successful = false;
				result.Exception = new DeleteArticleException("There was an error deleting the article", ex);
			}

			return result;
		}
	}
}
