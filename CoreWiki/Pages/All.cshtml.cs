using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class AllModel : PageModel
	{

		private readonly IArticleRepository _articleRepo;

		public AllModel(IArticleRepository articleRepo)
		{
			_articleRepo = articleRepo;
		}

		[FromRoute]
		public int PageNumber { get; set; } = 1;

		[BindProperty]
		public int PageSize { get; set; } = 10;

		public SelectList PageSizeOptions { get; set; }

		public int TotalPages { get; set; }

		public IEnumerable<Article> Articles { get; set; }


		public async Task OnGet(int pageNumber = 1)
		{
			ManagePageSize();
			Articles = await _articleRepo.GetAllArticlesPaged(PageSize, pageNumber);
			TotalPages = await _articleRepo.GetTotalPagesOfArticles(PageSize);
		}

		private void ManagePageSize()
		{
			if (int.TryParse(Request.Cookies["PageSize"], out var pageSize) == false)
			{
				pageSize = 20;
				Response.Cookies.Append("PageSize", pageSize.ToString());
			}
			var selectPageSizes = new List<int> { 2, 5, 10, 20, 40 };
			if (selectPageSizes.Contains(pageSize) == false)
			{
				selectPageSizes.Insert(0, pageSize);
			}
			PageSizeOptions = new SelectList(selectPageSizes);
			PageSize = pageSize;
		}

	}

}
