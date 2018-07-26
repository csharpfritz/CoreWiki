using Microsoft.Extensions.Logging;

namespace CoreWiki.Extensibility.Common
{
    public class CoreWikiModuleHost : ICoreWikiModuleHost
    {
        public CoreWikiModuleHost(ICoreWikiModuleEvents moduleEvents, ILoggerFactory loggerFactory)
        {
            Events = moduleEvents;
            LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// The events exposed to a CoreWiki module.
        /// </summary>
        public ICoreWikiModuleEvents Events { get; }

        /// <summary>
        /// The logger factory used to create a new logger within a CoreWiki module
        /// that will allow the CoreWiki module to send log messages back to CoreWiki.
        /// </summary>
        public ILoggerFactory LoggerFactory { get; }
    }
}
