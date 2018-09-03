using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Services;

namespace CoreWiki.Application.Articles.Queries
{

	public class GetArticlesToCreateFromArticleHandler : IRequestHandler<GetArticlesToCreateFromArticle, string[]>
	{
		private readonly IArticleReadingService _articleReadingService;

		public GetArticlesToCreateFromArticleHandler(IArticleReadingService articleReadingService)
		{
			_articleReadingService = articleReadingService;
		}

		public async Task<string[]> Handle(GetArticlesToCreateFromArticle request, CancellationToken cancellationToken)
		{
			var result = await _articleReadingService.GetArticlesToCreate(request.Slug);
			return result.ToArray();
		}

	}
}
