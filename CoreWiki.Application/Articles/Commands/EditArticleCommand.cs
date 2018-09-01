using MediatR;
using System;

namespace CoreWiki.Application.Articles.Commands
{
	public class EditArticleCommand : IRequest<CommandResult>
	{

		public int Id { get; }
		public string Topic { get; }
		public string Content { get; }
		public Guid AuthorId { get; set; }
		public string AuthorName { get; }

		public EditArticleCommand(int id, string topic, string content, Guid authorId, string authorName)
		{
			this.Id = id;
			this.Topic = topic;
			this.Content = content;
			this.AuthorId = authorId;
			this.AuthorName = authorName;
		}
	}

}
