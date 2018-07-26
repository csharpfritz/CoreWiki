using Microsoft.Extensions.Logging;

namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModuleHost
    {
        /// <summary>
        /// The events exposed to a CoreWiki module.
        /// </summary>
        ICoreWikiModuleEvents Events { get; }

        /// <summary>
        /// The logger factory used to create a new logger within a CoreWiki module
        /// that will allow the CoreWiki module to send log messages back to CoreWiki.
        /// </summary>
        ILoggerFactory LoggerFactory { get; }
    }
}