using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWiki.Application.Articles.Commands
{

	// NOTE:  We would rather have all of the primitive fields listed here instead of embedding a DTO

	public class CreateNewArticleCommand : IRequest<CommandResult>
	{
		public CreateNewArticleCommand(string topic, string slug, string content, Guid authorId, string authorName)
		{

			Topic = topic;
			Slug = slug;
			Content = content;
			AuthorId = authorId;
			AuthorName = authorName;

		}

		public string Topic { get; }

		public string Content { get; }

		public Guid AuthorId { get; }

		public string AuthorName { get; }

		public string Slug { get; }

	}
}
