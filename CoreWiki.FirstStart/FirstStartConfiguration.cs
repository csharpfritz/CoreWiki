using System.ComponentModel.DataAnnotations;

namespace CoreWiki.FirstStart
{
	public class FirstStartConfiguration
    {

		//public string WikiName { get; set; }

		[Required]
		public string AdminUserName { get; set; }

		[Required, EmailAddress]
		public string AdminEmail { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(6)]
		public string AdminPassword {get;set;}

		[Required]
		public string Database {get;set;}

		public string ConnectionString { get; set; }

    }


}
