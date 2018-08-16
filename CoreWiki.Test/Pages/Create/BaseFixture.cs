using CoreWiki.Core.Domain;
using CoreWiki.Core.Interfaces;
using CoreWiki.Pages;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreWiki.Test.Pages.Create
{
	public abstract class BaseFixture
	{

		protected const string _newArticleSlug = "new-page";
		protected const string _newArticleTopic = "New Page";
		protected const string username = "John Doe";
		protected Guid userId = Guid.NewGuid();
		protected readonly Mock<IArticleRepository> _articleRepo;
		protected Mock<IMediator> _mediator;
		protected CreateModel _sut;

		public BaseFixture()
		{

			_mediator = new Mock<IMediator>();
			_articleRepo = new Mock<IArticleRepository>();

		}

	}
}
