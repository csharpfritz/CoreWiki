using CoreWiki.Core.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain = CoreWiki.Core.Domain;

namespace CoreWiki.Data.Data.Repositories
{
	public class ArticleSqliteRepository : IArticleRepository
	{
		public ArticleSqliteRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }


		public async Task<IEnumerable<Domain.Article>> GetAllArticlesPaged(int pageSize, int pageNumber)
		{
			var articles = await Context.Articles
				.AsNoTracking()
				.OrderBy(a => a.Topic)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToArrayAsync();

				return articles.Select(a => a.ToDomain());
		}


		public async Task<List<Core.Domain.Article>> GetLatestArticles(int numOfArticlesToGet)
		{
			var articles = await Context.Articles
				.AsNoTracking()
				.OrderByDescending(a => a.Published).Take(numOfArticlesToGet).ToListAsync();
			return articles.Select(a => a.ToDomain()).ToList();
		}


		public async Task<int> GetTotalPagesOfArticles(int pageSize)
		{
			return (int)Math.Ceiling(await Context.Articles.CountAsync() / (double)pageSize);
		}


		public async Task<Core.Domain.Article> GetArticleBySlug(string articleSlug)
		{
			var article = await Context.Articles
				.AsNoTracking()
				.Include(a => a.Comments)
				.SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
			return article.ToDomain();
		}


		public async Task<Core.Domain.Article> GetArticleByComment(Domain.Comment comment)
		{
			var article = await Context.Articles
				.AsNoTracking()
				.Include(a => a.Comments)
				.SingleOrDefaultAsync(a => a.Id == comment.IdArticle);
			return article.ToDomain();
		}


		public async Task<Core.Domain.Article> GetArticleWithHistoriesBySlug(string articleSlug)
		{
			var article = await Context.Articles
				.AsNoTracking()
				.Include(a => a.History).SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
			return article.ToDomain();
		}


		public async Task<Core.Domain.Article> GetArticleById(int articleId)
		{
			var article = await Context.Articles.AsNoTracking().FirstAsync(a => a.Id == articleId);
			return article.ToDomain();
		}


		public async Task<Domain.Article> CreateArticleAndHistory(Domain.Article article)
		{

			var efArticle = ArticleDAO.FromDomain(article);

			Context.Articles.Add(efArticle);
			Context.ArticleHistories.Add(ArticleHistoryDAO.FromArticle(efArticle));
			await Context.SaveChangesAsync();
			return efArticle.ToDomain();
		}


		public async Task<bool> IsTopicAvailable(string articleSlug, int articleId)
		{
			return await Context.Articles
				.AsNoTracking()
				.AnyAsync(a => a.Slug == articleSlug && a.Id != articleId);
		}


		public IQueryable<Domain.Article> GetArticlesForSearchQuery(string filteredQuery)
		{

			// WARNING:  This may need to be further refactored to allow for database optimized search queries

			return Context.Articles
				.AsNoTracking()
				.Where(a =>
					a.Topic.ToUpper().Contains(filteredQuery.ToUpper()) ||
					a.Content.ToUpper().Contains(filteredQuery.ToUpper())
				).Select(a => a.ToDomain());
		}


		public void Dispose()
		{
			Context.Dispose();
		}

		public Task<bool> Exists(int id)
		{

			return Context.Articles.AnyAsync(e => e.Id == id);

		}

		public async Task Update(Core.Domain.Article article)
		{

			var efArticle = ArticleDAO.FromDomain(article);

			Context.Attach(efArticle).State = EntityState.Modified;

			Context.ArticleHistories.Add(ArticleHistoryDAO.FromArticle(efArticle));

			try
			{
				await Context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!await Exists(article.Id))
				{
					throw new ArticleNotFoundException();
				}
				else
				{
					throw;
				}
			}

		}

	}
}
