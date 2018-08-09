namespace CoreWiki.Data.EntityFramework.Models
{

	public static class ProtectedArticles {

		public static string[] ToArray() {

			return new [] {
				HomePage
			};

		}

		public const string HomePage = "home-page";


	}

}
