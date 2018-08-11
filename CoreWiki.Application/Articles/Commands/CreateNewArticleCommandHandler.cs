using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Helpers;
using CoreWiki.Core.Interfaces;
using CoreWiki.Core.Domain;
using MediatR;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Commands
{
	public class CreateNewArticleCommandHandler : IRequestHandler<CreateNewArticleCommand, List<string>>
	{
		private readonly IArticleRepository _articleRepo;
		private readonly IClock _clock;

		public CreateNewArticleCommandHandler(IArticleRepository articleRepo, IClock clock)
		{
			_articleRepo = articleRepo; _clock = clock;
		}
		public async Task<List<string>> Handle(CreateNewArticleCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var article = new Article
				{
					Topic = request.NewArticle.Topic,
					Slug = request.NewArticle.Slug,
					Content = request.NewArticle.Content,
					AuthorId = request.NewArticle.AuthorId,
					AuthorName = request.NewArticle.AuthorName,
					Published = _clock.GetCurrentInstant()
				};

				//DO IT!...
				var _createArticle = await _articleRepo.CreateArticleAndHistory(article);

				// ArticleHelpers / UrlHelpers / StringHelper was copied into the
				//CoreWiki.Application Common folder and namespace renamed _DUPLICATES

				var _articlesLinks = (await ArticleHelpers.GetArticlesToCreate(_articleRepo, article, createSlug: true))
				.ToList();


				return _articlesLinks;

			}
			catch (Exception)
			{
				throw new CreateArticleException();
			}
		}
	}
}
