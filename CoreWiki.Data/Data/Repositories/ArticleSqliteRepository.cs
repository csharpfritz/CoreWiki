using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Data.Data.Repositories
{
	public class ArticleSqliteRepository : IArticleRepository
	{
		public ArticleSqliteRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }


		public async Task<IEnumerable<Article>> GetAllArticlesPaged(int pageSize, int pageNumber)
		{
			return await Context.Articles
				.AsNoTracking()
				.OrderBy(a => a.Topic)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToArrayAsync();
		}


		public async Task<List<Article>> GetLatestArticles(int numOfArticlesToGet)
		{
			return await Context.Articles.OrderByDescending(a => a.Published).Take(numOfArticlesToGet).ToListAsync();
		}


		public async Task<int> GetTotalPagesOfArticles(int pageSize)
		{
			return (int)Math.Ceiling(await Context.Articles.CountAsync() / (double)pageSize);
		}


		public async Task<Article> GetArticleBySlug(string articleSlug)
		{
			return await Context.Articles.Include(a => a.Comments).SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
		}


		public async Task<Article> GetArticleByComment(Comment comment)
		{
			return await Context.Articles.Include(a => a.Comments)
				.SingleOrDefaultAsync(a => a.Id == comment.IdArticle);
		}


		public async Task<Article> GetArticleWithHistoriesBySlug(string articleSlug)
		{
			return await Context.Articles.Include(a => a.History).SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
		}


		public async Task<Article> GetArticleById(int articleId)
		{
			return await Context.Articles.AsNoTracking().FirstAsync(a => a.Id == articleId);
		}


		public async Task<Article> CreateArticleAndHistory(Article article)
		{

			Context.Articles.Add(article);
			Context.ArticleHistories.Add(ArticleHistory.FromArticle(article));
			await Context.SaveChangesAsync();
			return article;
		}


		public async Task<bool> IsTopicAvailable(string articleSlug, int articleId)
		{
			return await Context.Articles.AnyAsync(a => a.Slug == articleSlug && a.Id != articleId);
		}


		public IQueryable<Article> GetArticlesForSearchQuery(string filteredQuery)
		{
			return Context.Articles
				.AsNoTracking()
				.Where(a =>
					a.Topic.ToUpper().Contains(filteredQuery.ToUpper()) ||
					a.Content.ToUpper().Contains(filteredQuery.ToUpper())
				);
		}


		public void Dispose()
		{
			Context.Dispose();
		}

		public Task<bool> Exists(int id)
		{

			return Context.Articles.AnyAsync(e => e.Id == id);

		}

		public async Task Update(Article article)
		{

			Context.Attach(article).State = EntityState.Modified;

			Context.ArticleHistories.Add(ArticleHistory.FromArticle(article));

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
