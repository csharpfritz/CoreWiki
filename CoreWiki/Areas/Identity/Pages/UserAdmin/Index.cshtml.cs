using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWiki.Areas.Identity.Pages.UserAdmin
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<CoreWikiUser> UserManager;
		private readonly RoleManager<IdentityRole> RoleManager;

		public List<CoreWikiUser> UsersList { get; private set; }

		public List<IdentityRole> RolesList { get; private set; }

		public List<string> RoleNames { get; private set; } = new List<string>();

		public IndexModel(UserManager<CoreWikiUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			UserManager = userManager;
			RoleManager = roleManager;

			var currentRoles = roleManager.Roles.ToList();

			RolesList = currentRoles;
			UsersList = UserManager.Users.ToList();

			RoleNames = currentRoles.Select(r => r.Name).ToList();
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		#region AddRolesToUsers

		[BindProperty]
		public IEnumerable<string> UpdatedRoles { get; set; }

		[BindProperty]
		public string UsernameToAddRoleTo { get; set; }

		public async Task<IActionResult> OnPostUpdateUserRolesAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await UserManager.FindByEmailAsync(UsernameToAddRoleTo);

			if (user == null)
			{
				return Page();
			}

			foreach (var role in RoleNames)
			{
				if (UpdatedRoles.Contains(role))
				{
					await UserManager.AddToRoleAsync(user, role);
				}
				else
				{
					await UserManager.RemoveFromRoleAsync(user, role);
				}
			}

			return RedirectToPage("Index");
		}

		#endregion

		[BindProperty]
		public string RoleToRemove { get; set; }

		public async Task<IActionResult> OnPostDeleteRoleAsync()
		{
			var role = await RoleManager.FindByNameAsync(RoleToRemove);
			var result = await RoleManager.DeleteAsync(role);
			if (result.Succeeded)
			{
				return RedirectToPage();
			}

			return Page();
		}
	}
}
