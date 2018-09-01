using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Commands
{
	public class CreateNewCommentCommand : IRequest<CommandResult>
	{
		public Article Article { get; }
		public Comment Comment { get; }

		public CreateNewCommentCommand(Article article, Comment comment)
		{
			Article = article;
			Comment = comment;
		}
	}
}
