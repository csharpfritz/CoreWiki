using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWiki.Data.EntityFramework.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreWiki.FirstStart.MyFeature.Pages
{
	public class IndexModel : PageModel
	{

		public IndexModel(IHostingEnvironment env,
			IConfiguration config,
			UserManager<CoreWikiUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			this.Environment = env;
			this.Configuration = config;
			this.FirstStartConfig = new FirstStartConfiguration();

			this.UserManager = userManager;
			this.RoleManager = roleManager;

		}

		public IHostingEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		[BindProperty()]
		public FirstStartConfiguration FirstStartConfig { get; set; }
		public UserManager<CoreWikiUser> UserManager { get; }
		public RoleManager<IdentityRole> RoleManager { get; }
		public bool Completed { get; set; } = false;

		public void OnGet()
		{

		}

		[HttpPost()]
		public async Task<IActionResult> OnPostAsync()
		{

			if (!ModelState.IsValid)
			{
				return Page();
			}

			var newAdminUser = new CoreWikiUser
			{
				UserName = this.FirstStartConfig.AdminUserName,
				Email = this.FirstStartConfig.AdminEmail
			};

			var userResult = await UserManager.CreateAsync(newAdminUser, this.FirstStartConfig.AdminPassword);

			if (userResult.Succeeded)
			{
				var result = await UserManager.AddToRoleAsync(newAdminUser, "Administrators");
			}

			if (string.IsNullOrEmpty(FirstStartConfig.ConnectionString)) {
				FirstStartConfig.ConnectionString = "DataSource=./App_Data/wikiContent.db";
				FirstStartConfig.Database = "SQLite";
			}

			WriteConfigFileToDisk(this.FirstStartConfig.Database, this.FirstStartConfig.ConnectionString);

			Completed = true;
			return Page();

			//return RedirectToPage("/Details", new { slug = "home-page" });

		}


		private void WriteConfigFileToDisk(string provider, string connectionString)
		{

			// ramblinggeek cheered 500 bits on November 4, 2018


			var settingsFileLocation = Path.Combine(Environment.ContentRootPath, "appsettings.json");

			if (!System.IO.File.Exists(settingsFileLocation))
			{
				var fileStream = System.IO.File.Create(settingsFileLocation);
				var bytes = ASCIIEncoding.ASCII.GetBytes("{}");
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
				fileStream.Dispose();
			}
			var fileContents = System.IO.File.ReadAllText(settingsFileLocation);

			var jsonFile = JsonConvert.DeserializeObject<JObject>(fileContents);

			//jsonFile.Root.AddAfterSelf(JObject.Parse(@"{""foo"": ""bar""}").First);
			//jsonFile["foo"] = @"{v1: 1, v2: ""2"", v3: true}";
			jsonFile["DataProvider"] = provider;
			jsonFile["ConnectionStrings"]["CoreWikiData"] = connectionString;


			System.IO.File.WriteAllText(settingsFileLocation, JsonConvert.SerializeObject(jsonFile, Formatting.Indented));

		}
	}
}
