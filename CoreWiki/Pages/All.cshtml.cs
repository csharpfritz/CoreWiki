using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Pages
{
	public class AllModel : PageModel
	{

		private readonly ApplicationDbContext _Context;

		[BindProperty]
		public int PageSize { get; set; } = 5;


		private const int _PageSize = 2;

		public AllModel(ApplicationDbContext context)
		{
			this._Context = context;
		}

		[FromRoute]
		public int PageNumber { get; set; } = 1;

		public int TotalPages { get; set; }


		public IEnumerable<Article> Articles { get; set; }


		public async Task OnGet(int PageNumber = 1)
		{
			await FetchArticles();

		}

		public async Task OnPost()
		{
			await FetchArticles();
		}


		private async Task FetchArticles()
		{
			Articles = await _Context.Articles
				.AsNoTracking()
				.OrderBy(a => a.Topic)
				.Skip((PageNumber - 1) * PageSize)
				.Take(PageSize)
				.ToArrayAsync();

			TotalPages = (int)Math.Ceiling((await _Context.Articles.CountAsync()) / (double)PageSize);
		}

	}

}