using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Data.EntityFramework.Security
{
	public class CoreWikiIdentityContext : IdentityDbContext<CoreWikiUser>
	{
		public CoreWikiIdentityContext(DbContextOptions<CoreWikiIdentityContext> options)
				: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<IdentityRole>().HasData(new[]
			{
				new IdentityRole
				{
					Name = "Authors",
					NormalizedName = "Authors".ToUpper()
				},
				new IdentityRole
				{
					Name = "Editors",
					NormalizedName = "Editors".ToUpper()
				},
				new IdentityRole
				{
					Name = "Administrators",
					NormalizedName = "Administrators".ToUpper()
				}
			});
		}

		public static void SeedData(CoreWikiIdentityContext context)
		{

			context.Database.Migrate();

		}
	}
}
