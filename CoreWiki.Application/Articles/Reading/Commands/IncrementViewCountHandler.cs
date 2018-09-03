using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Data.Abstractions.Interfaces;
using MediatR;

//TODO gio pass repo

namespace CoreWiki.Application.Articles.Reading.Commands
{
	public class IncrementViewCountHandler : IRequestHandler<IncrementViewCountCommand>
	{
		private readonly IArticleRepository _repository;

		public IncrementViewCountHandler(IArticleRepository repository)
		{
			_repository = repository;
		}

		public async Task<Unit> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
		{

			await _repository.IncrementViewCount(request.Slug);
			return Unit.Value;

		}
	}
}
