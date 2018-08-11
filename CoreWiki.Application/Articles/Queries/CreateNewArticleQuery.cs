using CoreWiki.Application.Articles.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Queries
{
    public class CreateNewArticleQuery : IRequest<CreateArticleViewModel>
    {
		public CreateNewArticleQuery(string slug)
		{
			_slug = slug;
		}

		public string _slug { get; private set; }
	}
}
