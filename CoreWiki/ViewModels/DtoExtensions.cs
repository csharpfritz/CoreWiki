using CoreWiki.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.ViewModels
{
	public static class DtoExtensions
	{

		public static Core.Domain.Comment ToDomain(this Comment dto, Guid authorId)
		{

			return new Core.Domain.Comment
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
