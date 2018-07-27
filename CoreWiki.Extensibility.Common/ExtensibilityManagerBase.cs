using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CoreWiki.Extensibility.Common
{
	public abstract class ExtensibilityManagerBase
	{
		public const string ModulesPath = "CoreWikiModules";
		protected List<ICoreWikiModule> Modules;

		protected ExtensibilityManagerBase(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
		{
			RegisterCoreWikiModules(coreWikiModuleHost, moduleLoader);
		}

		private void RegisterCoreWikiModules(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
		{
			var rootModulesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var modulesPath = Path.Combine(rootModulesPath, ModulesPath);

			Modules = moduleLoader.Load(rootModulesPath, modulesPath);
			foreach (var coreWikiModule in Modules)
			{
				coreWikiModule.Initialize(coreWikiModuleHost);
			}
		}
	}
}
