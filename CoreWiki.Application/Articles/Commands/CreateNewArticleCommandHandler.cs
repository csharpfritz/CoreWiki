using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Helpers;
using CoreWiki.Core.Interfaces;
using CoreWiki.Core.Domain;
using MediatR;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace CoreWiki.Application.Articles.Commands
{
	public class CreateNewArticleCommandHandler : IRequestHandler<CreateNewArticleCommand, CommandResult>
	{
		private readonly IArticleRepository _articleRepo;
		private readonly IClock _clock;
		private readonly IMapper _mapper;

		public CreateNewArticleCommandHandler(IArticleRepository articleRepo, IClock clock, IMapper mapper)
		{
			_articleRepo = articleRepo;
			_clock = clock;
			_mapper = mapper;
		}

		public async Task<CommandResult> Handle(CreateNewArticleCommand request, CancellationToken cancellationToken)
		{
			var result = new CommandResult() { Successful = true };

			try
			{
				var article = _mapper.Map<Article>(request);
				article.Published = _clock.GetCurrentInstant();

				var _createArticle = await _articleRepo.CreateArticleAndHistory(article);
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
