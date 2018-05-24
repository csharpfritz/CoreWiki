using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages.Components.CreateComments
{
    [ViewComponent(Name = "CreateComments")]
    public class CreateComments : ViewComponent
    {
        private readonly CoreWiki.Models.ApplicationDbContext _context;

        public CreateComments(CoreWiki.Models.ApplicationDbContext context)
        {
            this._context = context;
        }

        public IViewComponentResult Invoke(Comment comment)
        {
            return View("CreateComments", comment);
        }
    }
}
