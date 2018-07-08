using CoreWiki.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Collections;

namespace CoreWiki.Test.EmailNotifications.GivenBadConfiguration
{

	public class WhenCreatingEmailNotifier
	{

		private readonly MockRepository _Mockery;
			
		public WhenCreatingEmailNotifier()
		{
			_Mockery = new MockRepository(MockBehavior.Default);
		}

		[Theory]
		[ClassData(typeof(InvalidConfiguration))]
		public void ThenShouldThrowException(Configuration.EmailNotifications config)
		{

			// Arrange
			var sgClient = _Mockery.Create<ISendGridClient>();

			// Assert
			Assert.Throws<ApplicationException>(() => new EmailNotifier(config, NullLoggerFactory.Instance, sgClient.Object));

		}

		public class InvalidConfiguration : IEnumerable<Object[]>
		{
			public IEnumerator<object[]> GetEnumerator()
			{

				yield return new[] { new Configuration.EmailNotifications { FromEmailAddress = "test@test.com", FromName = "Test McTester", SendGridApiKey = "" } };
				yield return new[] { new Configuration.EmailNotifications { FromEmailAddress = "test@test.com", FromName = "", SendGridApiKey = "1234" } };
				yield return new[] { new Configuration.EmailNotifications { FromEmailAddress = "", FromName = "Test McTester", SendGridApiKey = "1234" } };

			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		}

	}

}
