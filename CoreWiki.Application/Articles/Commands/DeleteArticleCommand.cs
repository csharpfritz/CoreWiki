using MediatR;

namespace CoreWiki.Application.Articles.Commands
{
	public class DeleteArticleCommand : IRequest<CommandResult>
	{
		public string Slug { get; }

		public DeleteArticleCommand(string slug)
		{
			Slug = slug;
		}
	}
}
