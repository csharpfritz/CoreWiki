using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Core.Configuration
{

	public class EmailNotifications
	{
		public string SendGridApiKey { get; set; }
		public string FromEmailAddress { get; set; }
		public string FromName { get; set; }
	}
}
