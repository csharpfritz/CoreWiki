using AutoMapper;
using CoreWiki.Application.Articles.Managing;
using Xunit;

namespace CoreWiki.Test.Helpers
{
	public class ArticleManagingProfileTests
	{
		private readonly IMapper _mapper;
		private readonly MapperConfiguration _mapperConfiguration;

		public ArticleManagingProfileTests()
		{
			_mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<ArticleManagingProfile>());
			_mapper = _mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void ConfigTest()
		{
			_mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}
