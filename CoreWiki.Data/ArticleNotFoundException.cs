using System;
using System.Runtime.Serialization;

namespace CoreWiki.Data
{
	[Serializable]
	public class ArticleNotFoundException : Exception
	{
		public ArticleNotFoundException()
		{
		}

		public ArticleNotFoundException(string message) : base(message)
		{
		}

		public ArticleNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ArticleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}