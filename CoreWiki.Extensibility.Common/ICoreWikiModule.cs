namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModule
    {
        /// <summary>
        /// Initializes a CoreWiki module with the CoreWiki module host.
        /// </summary>
        /// <param name="coreWikiModuleHost">The CoreWiki module host.</param>
        void Initialize(ICoreWikiModuleHost coreWikiModuleHost);
    }
}