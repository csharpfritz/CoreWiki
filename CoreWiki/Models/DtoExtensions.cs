using CoreWiki.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Models
{
	public static class DtoExtensions
	{

		public static Comment ToDomain(this CommentDTO dto, Guid authorId)
		{

			return new Comment
			{
				IdArticle = dto.ArticleId,
				Content = dto.Content,
				DisplayName = dto.DisplayName,
				Email = dto.Email,
				AuthorId = authorId
			};

		}


	}
}
