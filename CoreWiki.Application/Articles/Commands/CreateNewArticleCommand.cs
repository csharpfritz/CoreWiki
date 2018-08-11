using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Commands
{

	// NOTE:  We would rather have all of the primitive fields listed here instead of embedding a DTO

    public class CreateNewArticleCommand : IRequest<List<string>>
    {
		public CreateNewArticleCommand(ArticleCreateDTO newArticle)
		{
			NewArticle = newArticle;
		}

		public ArticleCreateDTO NewArticle { get; private set; }
	}
}
