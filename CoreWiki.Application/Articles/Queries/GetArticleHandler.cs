using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticleHandler : IRequestHandler<GetArticle, Core.Domain.Article>
	{

		public GetArticleHandler(IArticleRepository repository)
		{

			Repository = repository;

		}

		public IArticleRepository Repository { get; }

		public Task<Article> Handle(GetArticle request, CancellationToken cancellationToken)
		{

			return Repository.GetArticleBySlug(request.Slug);

		}

	}


}
