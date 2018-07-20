using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Areas.Identity
{
	public class AnyRoleRequirement : AuthorizationHandler<AnyRoleRequirement>, IAuthorizationRequirement
	{

		private string[] roles;

		public AnyRoleRequirement(string[] roles)
		{
			this.roles = roles;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AnyRoleRequirement requirement)
		{

			foreach (var role in roles)
			{

				if (context.User.IsInRole(role))
				{
					context.Succeed(requirement);
					break;
				}

			}

			return Task.CompletedTask;

		}
	}

	public static class RequirementExtensions
	{

		public static AuthorizationPolicyBuilder RequireAnyRole(this AuthorizationPolicyBuilder policy, params string[] roles)
		{

			return RequireAnyRole(policy, (IEnumerable<string>)roles);

		}

		public static AuthorizationPolicyBuilder RequireAnyRole(this AuthorizationPolicyBuilder policy, IEnumerable<string> roles)
		{

			policy.AddRequirements(new AnyRoleRequirement(roles.ToArray()));

			return policy;

		}

	}

}
