using AutoMapper;
using CoreWiki.Application.Articles.Search;
using Xunit;

namespace CoreWiki.Test.Application.Search
{
	public class ArticleSearchProfileTests
	{
		private readonly IMapper _mapper;
		private readonly MapperConfiguration _mapperConfiguration;

		public ArticleSearchProfileTests()
		{
			_mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<SearchArticleProfile>());
			_mapper = _mapperConfiguration.CreateMapper();
		}

		[Fact]
		public void ConfigTest()
		{
			_mapperConfiguration.AssertConfigurationIsValid();
		}
	}
}
