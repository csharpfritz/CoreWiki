using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Managing.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{ 
	class GetArticleHandler : IRequestHandler<GetIsTopicAvailableQuery, bool>,
		IRequestHandler<GetArticleQuery, ArticleManageDto>
	{
		private readonly IArticleManagementService _service;

		public GetArticleHandler(IArticleManagementService service)
		{
			_service = service;
		}

		public Task<bool> Handle(GetIsTopicAvailableQuery request, CancellationToken cancellationToken)
		{
			return _service.IsTopicAvailable(request.Slug, request.ArticleId);
		}

		public Task<ArticleManageDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
		{
			return _service.GetArticleBySlug(request.Slug);
		}
	}
}
