using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Managing.Dto;
using CoreWiki.Application.Articles.Managing.Events;
using CoreWiki.Application.Articles.Managing.Exceptions;
using CoreWiki.Application.Common;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Managing.Impl
{
	public class ArticleManagementService: IArticleManagementService
	{
		private static readonly string articleLinksPattern = @"(\[[\w\s.\-_:;\!\?]*[\]][\(])((?!(http|https))[\w\s\-_]*)([\)])";

		private readonly IArticleRepository _repository;
		private readonly ICommentRepository _commentRepository;
		private readonly ISlugHistoryRepository _slugHistoryRepository;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IClock _clock;

		public ArticleManagementService(IArticleRepository repository, ICommentRepository commentRepository, ISlugHistoryRepository slugHistoryRepository,IMediator mediator, IMapper mapper,  IClock clock)
		{
			_repository = repository;
			_commentRepository = commentRepository;
			_slugHistoryRepository = slugHistoryRepository;
			_mediator = mediator;
			_mapper = mapper;
			_clock = clock;
		}

		public async Task<Article> CreateArticleAndHistory(Article article)
		{
			article.Published = _clock.GetCurrentInstant();
			var createdArticle = await _repository.CreateArticleAndHistory(article);
			await _mediator.Publish(new ArticleCreatedNotification(createdArticle));
			return createdArticle;
		}

		public async Task<Article> Update(int id, string topic, string content, Guid authorId, string authorName)
		{
			var article = new Article { Topic = topic };
			if (string.IsNullOrWhiteSpace(article.Slug))
			{
				throw new InvalidTopicException("The topic must contain at least one alphanumeric character.");
			}

			var existingArticle = await _repository.GetArticleBySlug(article.Slug);
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

			return existingArticle;

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

		public Task<bool> IsTopicAvailable(string articleSlug, int articleId)
		{
			return _repository.IsTopicAvailable(articleSlug, articleId);
		}

		public async Task<ArticleManageDto> GetArticleBySlug(string articleSlug)
		{
			return _mapper.Map<ArticleManageDto>(await _repository.GetArticleBySlug(articleSlug));
		}

		public async Task<IList<string>> GetArticlesToCreate(string slug)
		{
			var articlesToCreate = new List<string>();
			var thisArticle = await GetArticleBySlug(slug);

			if (string.IsNullOrWhiteSpace(thisArticle.Content))
			{
				return articlesToCreate.Distinct().ToList();
			}

			foreach (var link in FindWikiArticleLinks(thisArticle.Content))
			{
				// Normalise the potential new wiki link into our slug format
				var newSlug = link;

				// Does the slug already exist in the database?
				if (!await IsTopicAvailable(slug, thisArticle.Id))
				{
					articlesToCreate.Add(slug);
				}
			}

			return articlesToCreate.Distinct().ToList();

			IEnumerable<string> FindWikiArticleLinks(string content)
			{
				return Regex.Matches(content, articleLinksPattern)
					.Select(match => match.Groups[2].Value)
					.ToArray();
			}
		}

		public async Task<(string,IList<string>)> GetArticlesToCreate(int articleId)
		{
			var articlesToCreate = new List<string>();
			var thisArticle = await _repository.GetArticleById(articleId);

			if (string.IsNullOrWhiteSpace(thisArticle.Content))
			{
				return (thisArticle.Slug,articlesToCreate.Distinct().ToList());
			}

			foreach (var link in FindWikiArticleLinks(thisArticle.Content))
			{
				// Normalise the potential new wiki link into our slug format
				var newSlug = link;

				// Does the slug already exist in the database?
				if (!await IsTopicAvailable(newSlug, thisArticle.Id))
				{
					articlesToCreate.Add(newSlug);
				}
			}

			return (thisArticle.Slug,articlesToCreate.Distinct().ToList());

			IEnumerable<string> FindWikiArticleLinks(string content)
			{
				return Regex.Matches(content, articleLinksPattern)
					.Select(match => match.Groups[2].Value)
					.ToArray();
			}
		}

		private bool Changed(string v1, string v2)
			=> !string.Equals(v1, v2, StringComparison.InvariantCulture);
	}
}
