using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreWiki.FirstStart.MyFeature.Pages
{
	public class IndexModel : PageModel
	{

		public IndexModel(IHostingEnvironment env, IConfiguration config)
		{
			this.Environment = env;
			this.Configuration = config;
		}

		public IHostingEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public void OnGet()
		{

			TestWritingFileToDisk();

		}
		// Test RG 29-09-2018 22:18
		private void TestWritingFileToDisk()
		{

			var settingsFileLocation = Path.Combine(Environment.ContentRootPath, "appsettings.app.json");

			if (!System.IO.File.Exists(settingsFileLocation)) {
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
			jsonFile["foo"] = "bar";


			System.IO.File.WriteAllText(settingsFileLocation, JsonConvert.SerializeObject(jsonFile, Formatting.Indented));

		}
	}
}
