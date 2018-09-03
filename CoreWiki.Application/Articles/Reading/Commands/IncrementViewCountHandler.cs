using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Commands
{
	public class IncrementViewCountHandler : IRequestHandler<IncrementViewCountCommand>
	{
		private readonly IArticleReadingService _service;

		public IncrementViewCountHandler(IArticleReadingService service)
		{
			_service = service;
		}

		public async Task<Unit> Handle(IncrementViewCountCommand request, CancellationToken cancellationToken)
		{

			await _service.IncrementViewCount(request.Slug);
			return Unit.Value;

		}
	}
}
