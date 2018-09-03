using MediatR;
using CoreWiki.Application.Articles.Services.Dto;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetSlugHistory : IRequest<SlugHistoryDto>
	{

		public GetSlugHistory(string historicalSlug)
		{
			this.HistoricalSlug = historicalSlug;
		}

		public string HistoricalSlug { get; }
	}


}
