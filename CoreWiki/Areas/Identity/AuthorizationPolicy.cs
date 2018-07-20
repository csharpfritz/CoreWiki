﻿using System.Collections.Generic;
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
				policy.RequireAnyRole("Authors", "Administrators");
			});
			
			// Authors can edit their own articles
			// Editors can edit anyones articles

		}
	}


}
