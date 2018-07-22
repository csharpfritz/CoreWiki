namespace CoreWiki.Extensibility.Common
{
    public abstract class ExtensibilityManagerBase
    {
        protected ExtensibilityManagerBase(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
        {
            OnRegisterCoreWikiModules(coreWikiModuleHost, moduleLoader);
        }

        protected internal virtual void OnRegisterCoreWikiModules(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
        {
        }
    }
}
