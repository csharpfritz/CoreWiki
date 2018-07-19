using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoreWiki.Data.Security
{
    // Add profile data for application users by adding properties to the CoreWikiUser class
    public class CoreWikiUser : IdentityUser
    {
		public bool CanNotify { get; set; }
    }
}
