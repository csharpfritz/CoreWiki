using System.IO;
using System.Reflection;

namespace CoreWiki.Extensibility.Common
{
    public abstract class ExtensibilityManagerBase
    {
        public const string ModulesPath = "CoreWikiModules";

        protected ExtensibilityManagerBase(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
        {
            RegisterCoreWikiModules(coreWikiModuleHost, moduleLoader);
        }

        private void RegisterCoreWikiModules(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
        {
            var rootModulesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var modulesPath = Path.Combine(rootModulesPath, ModulesPath);

            var modules = moduleLoader.Load(rootModulesPath, modulesPath);
            foreach (var coreWikiModule in modules)
            {
                coreWikiModule.Initialize(coreWikiModuleHost);
            }
        }
    }
}
