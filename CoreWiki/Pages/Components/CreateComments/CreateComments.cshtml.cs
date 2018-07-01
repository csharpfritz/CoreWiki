using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWiki.Pages.Components.CreateComments
{
	[ViewComponent(Name = "CreateComments")]
	public class CreateComments : ViewComponent
	{
		private readonly IApplicationDbContext _context;

		public CreateComments(IApplicationDbContext context)
		{
			this._context = context;
		}

		public IViewComponentResult Invoke(Comment comment)
		{
			return View("CreateComments", comment);
		}
	}
}
