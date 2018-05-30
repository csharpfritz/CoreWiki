using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages.Components.ListComments
{
    [ViewComponent(Name = "ListComments")]
    public class ListComments : ViewComponent
    {
        private readonly CoreWiki.Models.ApplicationDbContext _context;

        public ListComments(CoreWiki.Models.ApplicationDbContext context)
        {
            this._context = context;
        }

        public IViewComponentResult Invoke(ICollection<Comment> comments)
        {
            return View("ListComments", comments);
        }
    }
}
