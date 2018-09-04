using System;
using AutoMapper;
using CoreWiki.Configuration.Startup;
using CoreWiki.Pages;
using MediatR;
using Moq;

namespace CoreWiki.Test.Website.Pages.Create
{
	public abstract class BaseFixture
	{

		protected const string _newArticleSlug = "new-page";
		protected const string _newArticleTopic = "New Page";
		protected const string username = "John Doe";
		protected Guid userId = Guid.NewGuid();
		protected Mock<IMediator> _mediator;
		protected CreateModel _sut;
		protected MapperConfiguration _mapperConfiguration;
		protected IMapper _mapper;

		protected BaseFixture()
		{
			_mediator = new Mock<IMediator>();
			_mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CoreWikiWebsiteProfile>());
			_mapper = _mapperConfiguration.CreateMapper();
		}

	}
}
