using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Common
{
	public static class ConfigurePipeLineLogger
	{
		public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
		{
			private readonly ILogger _logger;

			public RequestLogger(ILogger<TRequest> logger)
			{
				_logger = logger;
			}

			public Task Process(TRequest request, CancellationToken cancellationToken)
			{
				var name = typeof(TRequest).Name;

				_logger.LogInformation("CoreWiki Request Query: {Name} {@Request}", name, request);

				return Task.CompletedTask;
			}
		}

	}
}
