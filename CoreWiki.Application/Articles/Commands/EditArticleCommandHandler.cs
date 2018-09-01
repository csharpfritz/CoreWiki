using System;
using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Helpers;
using CoreWiki.Core.Interfaces;
using MediatR;
using CoreWiki.Application.Articles.Exceptions;
using NodaTime;
using CoreWiki.Data.EntityFramework;
using System.Linq;

namespace CoreWiki.Application.Articles.Commands
{
	public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, CommandResult>
	{

		private readonly IArticleRepository _Repo;
		private readonly ISlugHistoryRepository _SlugRepo;
		private readonly IClock _clock;

		public EditArticleCommandHandler(IArticleRepository articleRepository, ISlugHistoryRepository slugHistoryRepository, IClock clock)
		{
			this._Repo = articleRepository;
			this._SlugRepo = slugHistoryRepository;
			this._clock = clock;
		}

		public async Task<CommandResult> Handle(EditArticleCommand editCommand, CancellationToken cancellationToken)
		{

			var result = new CommandResult() { Successful = false };

			var slug = UrlHelpers.URLFriendly(editCommand.Topic);
			if (String.IsNullOrWhiteSpace(slug))
			{
				result.Exception = new InvalidTopicException("The topic must contain at least one alphanumeric character.");
				return result;
			}

			var existingArticle = await _Repo.GetArticleBySlug(slug);
			if (existingArticle != null && existingArticle.Id != editCommand.Id)
			{
				result.Exception = new InvalidTopicException("The topic conflicts with an existing article.");
				return result;
			}

			if (existingArticle == null)
			{
				existingArticle = await _Repo.GetArticleById(editCommand.Id);
			}

			if (!Changed(existingArticle.Topic, editCommand.Topic) && !Changed(existingArticle.Content, editCommand.Content))
			{
				result.Exception = new NoContentChangedException();
				return result;
			}

			var oldSlug = existingArticle.Slug;

			existingArticle.Topic = editCommand.Topic;
			existingArticle.Slug = slug;
			existingArticle.Content = editCommand.Content;
			existingArticle.Version = existingArticle.Version + 1;
			existingArticle.Published = _clock.GetCurrentInstant();
			existingArticle.AuthorId = editCommand.AuthorId;
			existingArticle.AuthorName = editCommand.AuthorName;

			try
			{
				await _Repo.Update(existingArticle);

				if (Changed(oldSlug, existingArticle.Slug))
				{
					await _SlugRepo.AddToHistory(oldSlug, existingArticle);
				}
			}
			catch (ArticleNotFoundException ex)
			{
				result.Exception = ex;
				return result;
			}

			result.Successful = true;
			return result;

		}

		private bool Changed(string v1, string v2)
			=> !string.Equals(v1, v2, StringComparison.InvariantCulture);

	}

}
