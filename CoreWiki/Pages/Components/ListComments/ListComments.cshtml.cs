using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreWiki.Pages.Components.ListComments
{
	[ViewComponent(Name = "ListComments")]
	public class ListComments : ViewComponent
	{
		private readonly IApplicationDbContext _context;

		public ListComments(IApplicationDbContext context)
		{
			this._context = context;
		}

		public IViewComponentResult Invoke(ICollection<Comment> comments)
		{
			return View("ListComments", comments);
		}
	}
}
