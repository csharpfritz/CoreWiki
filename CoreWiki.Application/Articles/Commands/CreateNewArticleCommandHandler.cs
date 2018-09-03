using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Core.Domain;
using MediatR;
using NodaTime;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Services;

namespace CoreWiki.Application.Articles.Commands
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
			var result = new CommandResult() { Successful = true };

			try
			{
				var article = _mapper.Map<Article>(request);
				await _articleManagementService.CreateArticleAndHistory(article);
			}
			catch (Exception ex)
			{
				result.Successful = false;
				result.Exception = new CreateArticleException("There was an error creating the article", ex);
			}
			return result;

		}

	}
}
