using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Exceptions;
using CoreWiki.Application.Common;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Commands
{
	public class CreateNewArticleCommandHandler : IRequestHandler<CreateNewArticleCommand, CommandResult>
	{
		private readonly IArticleManagementService _articleManagementService;
		private readonly IMapper _mapper;

		public CreateNewArticleCommandHandler(IArticleManagementService articleManagementService, IMapper mapper)
		{
			_articleManagementService = articleManagementService;
			_mapper = mapper;
		}

		public async Task<CommandResult> Handle(CreateNewArticleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var article = _mapper.Map<Article>(request);
				await _articleManagementService.CreateArticleAndHistory(article);

				return CommandResult.Success();
			}
			catch (Exception ex)
			{
				return CommandResult.Error(new CreateArticleException("There was an error creating the article", ex));
			}
		}

	}
}
