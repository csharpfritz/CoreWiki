using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWiki.Areas.Identity.Pages.UserAdmin
{
	public class RolesModel : PageModel
	{
		[BindProperty]
		[Required]
		public string RoleName { get; set; }
		private readonly RoleManager<IdentityRole> RoleManager;

		public RolesModel(RoleManager<IdentityRole> roleManager)
		{
			RoleManager = roleManager;
		}


		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var result = await RoleManager.CreateAsync(new IdentityRole(RoleName));
			if (result.Errors.Any())
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return Page();
			}
			return RedirectToPage("Index");
		}
	}
}
