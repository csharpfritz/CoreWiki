using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{

	public class GetArticlesToCreateFromArticleHandler : IRequestHandler<GetArticlesToCreateFromArticleQuery, (string,string[])>
	{
		private readonly IArticleManagementService _articleReadingService;

		public GetArticlesToCreateFromArticleHandler(IArticleManagementService articleReadingService)
		{
			_articleReadingService = articleReadingService;
		}

		public async Task<(string,string[])> Handle(GetArticlesToCreateFromArticleQuery request, CancellationToken cancellationToken)
		{
			var result = await _articleReadingService.GetArticlesToCreate(request.ArticleId);
			return (result.Item1, result.Item2.ToArray());
		}

	}
}
