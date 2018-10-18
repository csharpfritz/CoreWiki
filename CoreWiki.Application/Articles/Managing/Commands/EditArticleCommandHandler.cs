using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Common;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Commands
{
	public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, CommandResult>
	{
		private readonly IArticleManagementService _articleManagementService;

		public EditArticleCommandHandler(IArticleManagementService articleManagementService)
		{
			_articleManagementService = articleManagementService;
		}

		public async Task<CommandResult> Handle(EditArticleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var theArticle = await _articleManagementService.Update(request.Id, request.Topic, request.Content, request.AuthorId, request.AuthorName);

				return CommandResult.Success(theArticle.Slug);
			}
			catch (Exception ex)
			{
				return CommandResult.Error(ex);
			}
		}
	}

}
