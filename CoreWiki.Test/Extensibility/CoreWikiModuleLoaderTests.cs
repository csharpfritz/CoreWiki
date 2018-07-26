using CoreWiki.Extensibility.Common;
using System.IO;
using System.Reflection;
using Xunit;

namespace CoreWiki.Test.Extensibility
{
    public class CoreWikiModuleLoaderTests
    {
        [Fact]
        public void Load_ReturnsExpectedCoreWikiModule()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var moduleLoader = new CoreWikiModuleLoader();
            var modules = moduleLoader.Load(path);

            //Assert.Single(modules);
            Assert.IsAssignableFrom<ICoreWikiModule>(modules[0]);
        }
    }
}
