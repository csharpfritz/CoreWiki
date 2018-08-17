using MediatR;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetSlugHistory : IRequest<Core.Domain.SlugHistory>
	{

		public GetSlugHistory(string historicalSlug)
		{
			this.HistoricalSlug = historicalSlug;
		}

		public string HistoricalSlug { get; }
	}


}
