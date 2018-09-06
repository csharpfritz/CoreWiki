using CoreWiki.Application.Articles.Reading.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetSlugHistoryQuery : IRequest<SlugHistoryDto>
	{

		public GetSlugHistoryQuery(string historicalSlug)
		{
			this.HistoricalSlug = historicalSlug;
		}

		public string HistoricalSlug { get; }
	}


}
