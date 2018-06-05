using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Models
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
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
		internal static void SeedData(CoreWikiIdentityContext context)
		{
			context.Database.EnsureCreated();
		}
	}
}
