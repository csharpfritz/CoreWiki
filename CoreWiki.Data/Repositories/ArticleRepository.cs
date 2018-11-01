using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using CoreWiki.Data.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Data.EntityFramework.Repositories
{
	public class ArticleRepository : IArticleRepository
	{
		public ArticleRepository(ApplicationDbContext context)
		{
			Context = context;
		}

		public ApplicationDbContext Context { get; }


		public async Task<List<Article>> GetLatestArticles(int numOfArticlesToGet)
		{
			var articles = await Context.Articles
				.AsNoTracking()
				.OrderByDescending(a => a.Published).Take(numOfArticlesToGet).ToListAsync();
			return articles.Select(a => a.ToDomain()).ToList();
		}


		public async Task<Article> GetArticleBySlug(string articleSlug)
		{
			var article = await Context.Articles
				.AsNoTracking()
				.Include(a => a.Comments)
				.SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
			return article?.ToDomain();
		}

		public async Task<Article> GetArticleWithHistoriesBySlug(string articleSlug)
		{
			var article = await Context.Articles
				.AsNoTracking()
				.Include(a => a.History).SingleOrDefaultAsync(m => m.Slug == articleSlug.ToLower());
			return article.ToDomain();
		}


		public async Task<Article> GetArticleById(int articleId)
		{
			var article = await Context.Articles.AsNoTracking().FirstOrDefaultAsync(a => a.Id == articleId);
			return article?.ToDomain();
		}


		public async Task<Article> CreateArticleAndHistory(Article article)
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

		public async Task<(IEnumerable<Article> articles, int totalFound)> GetArticlesForSearchQuery(string filteredQuery, int offset, int resultsPerPage)
		{
			// WARNING:  This may need to be further refactored to allow for database optimized search queries

			var articles = Context.Articles
				.AsNoTracking()
				.Where(a =>
					a.Topic.ToUpper().Contains(filteredQuery.ToUpper())
					|| a.Content.ToUpper().Contains(filteredQuery.ToUpper())
				).Select(a => a.ToDomain());
			var articleCount = articles.Count();
			var list = await articles.Skip(offset).Take(resultsPerPage).OrderByDescending(a => a.ViewCount).ToListAsync();

			return (list, articleCount);
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

		public async Task IncrementViewCount(string slug)
		{

			var article = Context.Articles.Single(a => a.Slug == slug);
			article.ViewCount++;
			await Context.SaveChangesAsync();

		}

		public async Task<Article> Delete(string slug)
		{
			var article = await Context.Articles
				.Include(o => o.History)
				.Include(o => o.Comments)
				.SingleOrDefaultAsync(o => o.Slug == slug);

			if (article != null)
			{
				Context.Articles.Remove(article);
				await Context.SaveChangesAsync();
			}

			return article?.ToDomain();
		}
	}
}
