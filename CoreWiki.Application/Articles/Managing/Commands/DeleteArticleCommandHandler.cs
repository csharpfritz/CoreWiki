using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing.Exceptions;
using CoreWiki.Application.Common;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Commands
{
	public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, CommandResult>
	{
		private readonly IArticleManagementService _articleManagementService;

		public DeleteArticleCommandHandler(IArticleManagementService articleManagementService)
		{
			_articleManagementService = articleManagementService;
		}

		public async Task<CommandResult> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				await _articleManagementService.Delete(request.Slug);
				return CommandResult.Success();
			}
			catch (Exception ex)
			{
				return CommandResult.Error(new DeleteArticleException("There was an error deleting the article", ex));
			}
		}
	}
}
