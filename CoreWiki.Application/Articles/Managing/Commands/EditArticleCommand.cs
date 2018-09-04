using System;
using CoreWiki.Application.Common;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Commands
{
	public class EditArticleCommand : IRequest<CommandResult>
	{

		public int Id { get; private set; }
		public string Topic { get; private set; }
		public string Content { get; private set; }
		public Guid AuthorId { get; private set; }
		public string AuthorName { get; private set; }

		//public EditArticleCommand()
		//{
			
		//}

		//public EditArticleCommand(int id, string topic, string content, Guid authorId, string authorName)
		//{
		//	this.Id = id;
		//	this.Topic = topic;
		//	this.Content = content;
		//	this.AuthorId = authorId;
		//	this.AuthorName = authorName;
		//}
	}

}
