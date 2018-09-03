using System;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Exceptions;
using CoreWiki.Application.Articles.Notifications;
using CoreWiki.Application.Helpers;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Services.Impl
{
	public class ArticleManagementService: IArticleManagementService
	{
		private readonly IArticleRepository _repository;
		private readonly ICommentRepository _commentRepository;
		private readonly ISlugHistoryRepository _slugHistoryRepository;
		private readonly IMediator _mediator;
		private readonly IClock _clock;

		public ArticleManagementService(IArticleRepository repository, ICommentRepository commentRepository, ISlugHistoryRepository slugHistoryRepository,IMediator mediator,  IClock clock)
		{
			_repository = repository;
			_commentRepository = commentRepository;
			_slugHistoryRepository = slugHistoryRepository;
			_mediator = mediator;
			_clock = clock;
		}

		public async Task<Article> CreateArticleAndHistory(Article article)
		{
			article.Published = _clock.GetCurrentInstant();
			var createdArticle = await _repository.CreateArticleAndHistory(article);
			await _mediator.Publish(new ArticleCreatedNotification(createdArticle));
			return createdArticle;
		}

		public async Task Update(int id, string topic, string content, Guid authorId, string authorName)
		{
			var slug = UrlHelpers.URLFriendly(topic);
			if (string.IsNullOrWhiteSpace(slug))
			{
				throw new InvalidTopicException("The topic must contain at least one alphanumeric character.");
			}

			var existingArticle = await _repository.GetArticleBySlug(slug);
			if (existingArticle != null && existingArticle.Id != id)
			{
				throw new InvalidTopicException("The topic conflicts with an existing article.");
			}

			existingArticle = await _repository.GetArticleById(id);

			if (!Changed(existingArticle.Topic, topic) && !Changed(existingArticle.Content, content))
			{
				throw new NoContentChangedException();
			}

			var oldSlug = existingArticle.Slug;
			existingArticle.Topic = topic;
			existingArticle.Slug = UrlHelpers.URLFriendly(topic);
			existingArticle.Content = content;
			existingArticle.AuthorId = authorId;
			existingArticle.AuthorName = authorName;

			existingArticle.Version++;
			existingArticle.Published = _clock.GetCurrentInstant();
			await _repository.Update(existingArticle);
			if (Changed(oldSlug, existingArticle.Slug))
			{
				await _slugHistoryRepository.AddToHistory(oldSlug, existingArticle);
			}
			await _mediator.Publish(new ArticleEditedNotification(existingArticle));
		}

		public async Task<Article> Delete(string slug)
		{
			var article = await _repository.GetArticleBySlug(slug);
			await _commentRepository.DeleteAllCommentsOfArticle(article.Id);
			await _slugHistoryRepository.DeleteAllHistoryOfArticle(article.Id);			
			var deletedArticle = await _repository.Delete(slug);
			await _mediator.Publish(new ArticleDeletedNotification(deletedArticle));
			return article;
		}

		private bool Changed(string v1, string v2)
			=> !string.Equals(v1, v2, StringComparison.InvariantCulture);
	}
}
