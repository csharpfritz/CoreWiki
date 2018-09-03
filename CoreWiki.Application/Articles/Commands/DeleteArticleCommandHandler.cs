using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Services;
using MediatR;

namespace CoreWiki.Application.Articles.Commands
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
			var result = new CommandResult() { Successful = true };

			try
			{
				await _articleManagementService.Delete(request.Slug);
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
