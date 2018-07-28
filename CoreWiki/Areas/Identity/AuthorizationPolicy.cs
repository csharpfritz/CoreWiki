using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Areas.Identity
{
	public class AuthPolicy
	{

		internal static void Execute(AuthorizationOptions options)
		{

			options.AddPolicy(PolicyConstants.CanDeleteArticles, policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireRole("Administrators");
			});

			options.AddPolicy(PolicyConstants.CanCreateComments, policy =>
			{
				policy.RequireAuthenticatedUser();
			});

			options.AddPolicy(PolicyConstants.CanWriteArticles, policy =>
			{
				policy.RequireAuthenticatedUser();
				// TODO: Re-enable when we can assign users to a role
				// policy.RequireAnyRole("Authors", "Administrators");
			});

			options.AddPolicy(PolicyConstants.CanEditArticles, policy =>
			{
				policy.RequireAuthenticatedUser();

				// TODO: After we can assign users to roles, we will want to re-enable this

				// policy.RequireAnyRole("Authors", "Administrators");
			});

			// Authors can edit their own articles
			// Editors can edit anyones articles

		}
	}


}
