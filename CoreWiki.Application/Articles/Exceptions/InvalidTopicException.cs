using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CoreWiki.Application.Articles.Exceptions
{
	[Serializable]
	public class InvalidTopicException : Exception
	{

		public InvalidTopicException(string message) : base(message) { }

		public InvalidTopicException(string message, Exception innerException) : base(message, innerException) { }

		protected InvalidTopicException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}


}
