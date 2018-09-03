using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Data.Abstractions.Interfaces;
using MediatR;

//TODO gio pass repo

namespace CoreWiki.Application.Articles.Commands
{
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
