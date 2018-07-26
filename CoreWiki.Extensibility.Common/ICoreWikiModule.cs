using System;

namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModule
    {
        void Initialize(CoreWikiModuleEvents moduleEvents);
    }
}
