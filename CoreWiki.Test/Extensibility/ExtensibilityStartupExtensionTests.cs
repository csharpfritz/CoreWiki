using CoreWiki.Extensibility.Common;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CoreWiki.Test.Extensibility
{
	public class ExtensibilityStartupExtensionTests
	{
		[Fact]
		public void ConfigureExtensibility_AddsRequiredServices()
		{
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddLogging();

			serviceCollection.ConfigureExtensibility();
			var serviceProvider = serviceCollection.BuildServiceProvider();

			Assert.NotNull(serviceProvider.GetService<ICoreWikiModuleHost>());
			Assert.NotNull(serviceProvider.GetService<IExtensibilityManager>());
			Assert.NotNull(serviceProvider.GetService<ICoreWikiModuleEvents>());
			Assert.NotNull(serviceProvider.GetService<ICoreWikiModuleLoader>());
		}
	}
}
