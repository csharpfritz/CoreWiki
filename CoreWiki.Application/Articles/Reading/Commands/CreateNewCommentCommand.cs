using System;
using CoreWiki.Application.Articles.Commands;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Reading.Commands
{
	public class CreateNewCommentCommand : IRequest<CommandResult>
	{
		public int ArticleID { get; set; }
		public Instant Submitted { get; set; }
		public Guid AuthorId { get; set; }
		public string Content { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
	}
}
