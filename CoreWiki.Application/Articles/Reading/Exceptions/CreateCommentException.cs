using System;

namespace CoreWiki.Application.Articles.Reading.Exceptions
{
	[Serializable]
	internal class CreateCommentException : Exception
	{
		public CreateCommentException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
