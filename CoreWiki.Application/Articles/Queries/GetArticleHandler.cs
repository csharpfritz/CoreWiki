using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticleHandler
		: IRequestHandler<GetArticle, Article>,
		  IRequestHandler<GetArticleById, Article>
	{
		public IArticleRepository Repository { get; }

		public GetArticleHandler(IArticleRepository repository)
		{
			Repository = repository;
		}

		public Task<Article> Handle(GetArticle request, CancellationToken cancellationToken)
		{
			return Repository.GetArticleBySlug(request.Slug);
		}

		public Task<Article> Handle(GetArticleById request, CancellationToken cancellationToken)
		{
			return Repository.GetArticleById(request.Id);
		}
	}


}
