using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Application.Articles.Reading.Events;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using MediatR;
using NodaTime;

namespace CoreWiki.Application.Articles.Reading.Impl
{
	public class ArticleReadingService: IArticleReadingService
	{
		private readonly IArticleRepository _repository;
		private readonly ISlugHistoryRepository _slugHistoryRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IClock _clock;

		public ArticleReadingService(IArticleRepository repository,
			ISlugHistoryRepository slugHistoryRepository,
			ICommentRepository commentRepository,
			IMediator mediator,
			IMapper mapper,
			IClock clock)
		{
			_repository = repository;
			_slugHistoryRepository = slugHistoryRepository;
			_commentRepository = commentRepository;
			_mediator = mediator;
			_mapper = mapper;
			_clock = clock;
		}

		public async Task<ArticleReadingDto> GetArticleBySlug(string articleSlug)
		{
			return _mapper.Map<ArticleReadingDto>(await _repository.GetArticleBySlug(articleSlug));
		}

		public Task<bool> IsTopicAvailable(string articleSlug, int articleId)
		{
			return _repository.IsTopicAvailable(articleSlug, articleId);
		}

		public async Task<SlugHistoryDto> GetSlugHistoryWithArticle(string slug)
		{
			return _mapper.Map<SlugHistoryDto>(await _slugHistoryRepository.GetSlugHistoryWithArticle(slug));
		}

		public async Task CreateComment(CreateCommentDto commentDto)
		{
			var comment = _mapper.Map<Comment>(commentDto);
			comment.Submitted = _clock.GetCurrentInstant();
			await _commentRepository.CreateComment(comment);
			var article = await _repository.GetArticleById(comment.ArticleId);
			await _mediator.Publish(new CommentPostedNotification(article, comment));
		}

		public async Task<ArticleReadingDto> GetArticleById(int articleId)
		{
			return _mapper.Map<ArticleReadingDto>(await _repository.GetArticleById(articleId));
		}

		public async Task<ArticleReadingDto> GetArticleWithHistoriesBySlug(string articleSlug)
		{
			return _mapper.Map<ArticleReadingDto>(await _repository.GetArticleWithHistoriesBySlug(articleSlug));
		}

		public async Task<List<ArticleReadingDto>> GetLatestArticles(int numOfArticlesToGet)
		{
			return _mapper.Map<List<ArticleReadingDto>>(await _repository.GetLatestArticles(numOfArticlesToGet));
		}

		public Task IncrementViewCount(string slug)
		{
			return _repository.IncrementViewCount(slug);
		}
	}
}
