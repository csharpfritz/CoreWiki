using System;
using System.Runtime.Serialization;

namespace CoreWiki.Application.Articles.Managing.Exceptions
{
	[Serializable]
	internal class CreateArticleException : Exception
	{
		public CreateArticleException()
		{
		}

		public CreateArticleException(string message) : base(message)
		{
			//Log error
		}

		public CreateArticleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CreateArticleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
