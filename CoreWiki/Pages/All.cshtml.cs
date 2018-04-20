using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Pages
{
	public class AllModel : PageModel
	{

		private readonly ApplicationDbContext _Context;

		public AllModel(ApplicationDbContext context)
		{
			this._Context = context;
		}

		[FromRoute]
		public int PageNumber { get; set; } = 1;

		[BindProperty]
		public int PageSize { get; set; }

		public SelectList PageSizeOptions { get; set; }

		public int TotalPages { get; set; }

		public IEnumerable<Article> Articles { get; set; }


		public async Task OnGet(int PageNumber = 1)
		{
			ManagePageSize();
			await FetchArticles();
		}

		private void ManagePageSize()
		{
			if (int.TryParse(Request.Cookies["PageSize"], out int pageSize) == false)
			{
				pageSize = 20;
				Response.Cookies.Append("PageSize", pageSize.ToString());
			}
			List<int> selectPageSizes = new List<int> { 2, 5, 10, 20, 40 };
			if (selectPageSizes.Contains(pageSize) == false)
			{
				selectPageSizes.Insert(0, pageSize);
			}
			PageSizeOptions = new SelectList(selectPageSizes);
			PageSize = pageSize;
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