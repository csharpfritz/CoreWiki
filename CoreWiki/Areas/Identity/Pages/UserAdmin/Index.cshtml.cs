using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Data.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreWiki.Areas.Identity.Pages.UserAdmin
{
	public class IndexModel : PageModel
	{
		private readonly UserManager<CoreWikiUser> UserManager;
		private readonly RoleManager<IdentityRole> RoleManager;

		public List<SelectListItem> Roles { get; } = new List<SelectListItem>();

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

			foreach (var role in currentRoles)
			{
				Roles.Add(new SelectListItem { Text = role.Name, Value = role.NormalizedName });
				RoleNames.Add(role.NormalizedName);
			}
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		#region AddRolesToUsers

		[BindProperty]
		public string RoleToAdd { get; set; }
		[BindProperty]
		public string UsernameToAddRoleTo { get; set; }

		public async Task<IActionResult> OnPostAddRoleToUserAsync()
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

			var roleToUserResult = await UserManager.AddToRoleAsync(user, RoleToAdd);
			if (!roleToUserResult.Succeeded)
			{
				return StatusCode(500);
			}
			return RedirectToPage("Index");
		}

		public async Task<IActionResult> OnPostAddAllRolesToUserAsync()
		{
			var user = await UserManager.FindByEmailAsync(UsernameToAddRoleTo);

			if (user == null)
			{
				return Page();
			}

			foreach (var role in RoleNames)
			{
				await UserManager.AddToRoleAsync(user, role);
			}

			return RedirectToPage("Index");

		}
		#endregion

		#region RemoveRolesFromUsers

		[BindProperty]
		public string RoleToRemove { get; set; }
		[BindProperty]
		public string UsernameToRemoveRoleFrom { get; set; }

		public async Task<IActionResult> OnPostRemoveRoleFromUserAsync()
		{
			var user = await UserManager.FindByEmailAsync(UsernameToRemoveRoleFrom);

			if (user == null)
			{
				return Page();
			}

			await UserManager.RemoveFromRoleAsync(user, RoleToRemove);

			return RedirectToPage("Index");
		}
		public async Task<IActionResult> OnPostRemoveAllRolesFromUserAsync()
		{
			var user = await UserManager.FindByEmailAsync(UsernameToRemoveRoleFrom);

			if (user == null)
			{
				return Page();
			}

			foreach (var role in RoleNames)
			{
				await UserManager.RemoveFromRoleAsync(user, role);
			}

			return RedirectToPage("Index");
		}
		#endregion

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
