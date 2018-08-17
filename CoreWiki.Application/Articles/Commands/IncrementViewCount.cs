using CoreWiki.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Commands
{
	public class IncrementViewCount : IRequest
	{

		public IncrementViewCount(string slug)
		{
			this.Slug = slug;
		}

		public string Slug { get; }

	}

	public class IncrementViewCountHandler : IRequestHandler<IncrementViewCount>
	{
		private readonly IArticleRepository _repository;

		public IncrementViewCountHandler(IArticleRepository repository)
		{
			_repository = repository;
		}

		public async Task<Unit> Handle(IncrementViewCount request, CancellationToken cancellationToken)
		{

			await _repository.IncrementViewCount(request.Slug);
			return Unit.Value;

		}
	}


}
