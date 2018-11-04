using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreWiki
{
	public class Program
	{

		private static IWebHost _Host;
		private static bool _Restart = true;

		public static void Main(string[] args)
		{

			while (_Restart) {

				_Restart = false;
				_Host = BuildWebHost(args);
				_Host.Run();

			}

		}

		public static Task Restart() {

			_Restart = true;
			return _Host.StopAsync();

		}

		public static IWebHost BuildWebHost(string[] args) =>
				WebHost.CreateDefaultBuilder(args)
						.UseApplicationInsights()
						.UseStartup<Startup>()
						.UseKestrel(options =>
						{
							options.AddServerHeader = false;
						})
						.Build();
	}
}
