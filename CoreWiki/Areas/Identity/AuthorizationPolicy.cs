using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Areas.Identity
{
	public class AuthPolicy
	{
		/// <summary>
		/// Requires the user to be logged in and have the Administrator role
		/// </summary>
		/// <value>
		/// The require administrator role.
		/// </value>
		public static AuthorizationPolicy RequireAdministratorRole => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Administrators").Build();


		/// <summary>
		/// Requires the user to be logged in.
		/// </summary>
		/// <value>
		/// The require logged in user.
		/// </value>
		public static AuthorizationPolicy RequireLoggedInUser => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

		/// <summary>
		/// Requires the user to be logged in and be in one or more of the required roles specified.
		/// </summary>
		/// <param name="roles">The roles.</param>
		/// <returns></returns>
		public static AuthorizationPolicy RequireUserWithEitherRole(string[] roles) => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireAnyRole(roles).Build();

		internal static void Execute(AuthorizationOptions options)
		{
			options.AddPolicy(PolicyConstants.CanDeleteArticles, RequireAdministratorRole);
			options.AddPolicy(PolicyConstants.CanManageRoles, RequireAdministratorRole);
			options.AddPolicy(PolicyConstants.CanDeleteArticles, RequireAdministratorRole);
			options.AddPolicy(PolicyConstants.CanManageRoles, RequireAdministratorRole);
			options.AddPolicy(PolicyConstants.CanCreateComments, RequireLoggedInUser);
			options.AddPolicy(PolicyConstants.CanWriteArticles, RequireUserWithEitherRole(new[] { "Authors", "Administrators" }));
			options.AddPolicy(PolicyConstants.CanEditArticles, RequireUserWithEitherRole(new[] {"Authors", "Administrators"}));

			// Authors can edit their own articles
			// Editors can edit anyones articles

		}
	}
}
