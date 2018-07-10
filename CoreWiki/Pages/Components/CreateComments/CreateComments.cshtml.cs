using CoreWiki.Data;
using CoreWiki.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWiki.Pages.Components.CreateComments
{
	[ViewComponent(Name = "CreateComments")]
	public class CreateComments : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public CreateComments(IApplicationDbContext context)
		{
		this._context = (ApplicationDbContext)context;
		}

		public IViewComponentResult Invoke(Comment comment)
		{
			return View("CreateComments", comment);
		}
	}
}
