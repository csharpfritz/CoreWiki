using System;
using CoreWiki.Application.Articles.Services.Dto;
using CoreWiki.Core.Domain;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Commands
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
