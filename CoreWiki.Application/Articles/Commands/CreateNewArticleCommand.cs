using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Commands
{
    public class CreateNewArticleCommand : IRequest<List<string>>
    {
		public CreateNewArticleCommand(ArticleCreateDTO newArticle)
		{
			NewArticle = newArticle;
		}

		public ArticleCreateDTO NewArticle { get; private set; }
	}
}
