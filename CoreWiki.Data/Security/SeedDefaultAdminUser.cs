using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoreWiki.Data.EntityFramework.Security
{
	//Todo - This is a bit of a hack to ensure that there is a default admin user on first run, can probably remove when the first start wizard is implemented
	public static class SeedDefaultAdminUserToAdminRole
	{
		public static async Task Seed(UserManager<CoreWikiUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var administratorsRole = "Administrators";
			var defaultAdminUsername = "admin@corewiki.com";
			var defaultAdminPassword = "Admin@123";

			var adminRoleExists = await roleManager.RoleExistsAsync(administratorsRole);

			if (!adminRoleExists)
			{
				var role = new IdentityRole(administratorsRole)
				{
					Name = administratorsRole
				};

				var roleResult = await roleManager.CreateAsync(role);
			}

			// If there are no users who are currently an admin, then create a default admin user
			// var anyAdminUsers = await userManager.GetUsersInRoleAsync(administratorsRole);

			// if (!anyAdminUsers.Any())
			// {
			// 	var defaultAdminUserExists = await userManager.FindByEmailAsync(defaultAdminUsername);

			// 	if (defaultAdminUserExists == null)
			// 	{
			// 		var defaultAdminUser = new CoreWikiUser
			// 		{
			// 			UserName = defaultAdminUsername,
			// 			Email = defaultAdminUsername
			// 		};

			// 		var userResult = await userManager.CreateAsync(defaultAdminUser, defaultAdminPassword);

			// 		if (userResult.Succeeded)
			// 		{
			// 			var result = await userManager.AddToRoleAsync(defaultAdminUser, administratorsRole);
			// 		}
			// 	}
			// }
		}
	}
}
