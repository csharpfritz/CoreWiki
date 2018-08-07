using CoreWiki.Data;
using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreWiki.Pages.Components.CreateComments
{
	[ViewComponent(Name = "CreateComments")]
	public class CreateComments : ViewComponent
	{
		public IViewComponentResult Invoke(CommentDTO comment)
		{
			return View("CreateComments", comment);
		}
	}
}
