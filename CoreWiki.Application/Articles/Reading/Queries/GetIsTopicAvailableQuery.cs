using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetIsTopicAvailableQuery : IRequest<bool>
	{
		public string Slug { get; set; }
		public int ArticleId { get; set; }
	}
}
