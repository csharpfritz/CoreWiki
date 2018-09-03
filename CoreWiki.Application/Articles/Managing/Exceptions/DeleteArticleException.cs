using System;

namespace CoreWiki.Application.Articles.Managing.Exceptions
{
	[Serializable]
	internal class DeleteArticleException : Exception
	{
		public DeleteArticleException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
