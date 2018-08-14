using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CoreWiki.Test.Pages
{
	public class FakeMediator : IMediator
	{
		public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
		{
			// do nothing
			return Task.CompletedTask;
		}

		private readonly object _fakeReponse;

		public FakeMediator(object fakeResponse)
		{
			_fakeReponse = fakeResponse;
		}

		public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult((TResponse)_fakeReponse);
		}
	}


}
