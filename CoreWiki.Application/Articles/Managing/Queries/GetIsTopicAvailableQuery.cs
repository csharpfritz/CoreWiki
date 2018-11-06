using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{
	public class GetIsTopicAvailableQuery : IRequest<bool>
	{
		public string Topic { get; set; }
		public int ArticleId { get; set; }
	}
}
