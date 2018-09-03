using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{

	public class GetArticlesToCreateFromArticleHandler : IRequestHandler<GetArticlesToCreateFromArticleQuery, string[]>
	{
		private readonly IArticleReadingService _articleReadingService;

		public GetArticlesToCreateFromArticleHandler(IArticleReadingService articleReadingService)
		{
			_articleReadingService = articleReadingService;
		}

		public async Task<string[]> Handle(GetArticlesToCreateFromArticleQuery request, CancellationToken cancellationToken)
		{
			var result = await _articleReadingService.GetArticlesToCreate(request.Slug);
			return result.ToArray();
		}

	}
}
