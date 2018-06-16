using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Areas.Identity
{
	public class AuthorizationPolicy
	{
		internal static void Execute(AuthorizationOptions options)
		{

			options.AddPolicy("CanDeleteArticles", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireRole("Administrator");
			});

			// Logged in users can write comments
			// Authors can write articles
			// Authors can edit their own articles
			// Editors can edit anyones articles
			// Administrators can delete articles


		}
	}
}
