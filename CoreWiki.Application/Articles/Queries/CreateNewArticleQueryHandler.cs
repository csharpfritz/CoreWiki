using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Models;
using CoreWiki.Data.Data.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Queries
{
	public class CreateNewArticleQueryHandler : IRequestHandler<CreateNewArticleQuery, CreateArticleViewModel>
	{
		public CreateNewArticleQueryHandler()
		{
			
		}

		public async Task<CreateArticleViewModel> Handle(CreateNewArticleQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var _returnModel = new CreateArticleViewModel() { NewArticle = new ArticleCreateDTO() };

				_returnModel.NewArticle.Topic = request._slug;

				return await Task.Run(() => _returnModel);
			}
			catch (Exception)
			{
			    throw new CreateArticleException();
			}
			
		}
	}
}
