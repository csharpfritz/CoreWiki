using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreWiki.Configuration.Startup;
using Xunit;

namespace CoreWiki.Test.Helpers
{
	public class CoreWikiWebsiteProfileTests
	{
		private readonly IMapper _mapper;
		private readonly MapperConfiguration _mapperConfiguration;

		public CoreWikiWebsiteProfileTests()
		{
			_mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<CoreWikiWebsiteProfile>());
			_mapper = _mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void ConfigTest()
		{
			_mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}
