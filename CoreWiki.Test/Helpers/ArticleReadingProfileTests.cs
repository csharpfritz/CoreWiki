using AutoMapper;
using CoreWiki.Application.Common.MappingProfiles;
using Xunit;

namespace CoreWiki.Test.Helpers
{
	public class ArticleReadingProfileTests
	{
		private readonly IMapper _mapper;
		private readonly MapperConfiguration _mapperConfiguration;

		public ArticleReadingProfileTests()
		{
			_mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ArticleReadingProfile>());
			_mapper = _mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void ConfigTest()
		{
			_mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}
