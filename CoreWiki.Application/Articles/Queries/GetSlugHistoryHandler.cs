using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetSlugHistoryHandler : IRequestHandler<GetSlugHistory, SlugHistory>
	{
		private readonly ISlugHistoryRepository _repository;

		public GetSlugHistoryHandler(ISlugHistoryRepository repository) {

			_repository = repository;

		}

		public async Task<SlugHistory> Handle(GetSlugHistory request, CancellationToken cancellationToken)
		{
			return await _repository.GetSlugHistoryWithArticle(request.HistoricalSlug);
		}
	}


}
