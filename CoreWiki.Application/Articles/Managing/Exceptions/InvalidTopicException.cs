using System;
using System.Runtime.Serialization;

namespace CoreWiki.Application.Articles.Managing.Exceptions
{
	[Serializable]
	public class InvalidTopicException : Exception
	{

		public InvalidTopicException(string message) : base(message) { }

		public InvalidTopicException(string message, Exception innerException) : base(message, innerException) { }

		protected InvalidTopicException(SerializationInfo info, StreamingContext context) : base(info, context) { }

	}


}
