using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoreWiki.Areas.Identity.Pages.Account.Manage
{
    public class MyRolesDataModel : PageModel
    {
        private readonly UserManager<CoreWikiUser> _userManager;
		private readonly ILogger<MyRolesDataModel> _logger;

        public MyRolesDataModel(
            UserManager<CoreWikiUser> userManager,
			RoleManager<IdentityRole> roleManager,
            ILogger<MyRolesDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

	    public IList<string> Roles { get; set; }

		public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

	        Roles = await _userManager.GetRolesAsync(user);
	        
            return Page();
        }
    }
}
