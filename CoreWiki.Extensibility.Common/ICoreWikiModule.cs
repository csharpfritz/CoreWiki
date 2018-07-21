namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModule
    {
        void Initialize(CoreWikiModuleEvents moduleEvents);
    }
}