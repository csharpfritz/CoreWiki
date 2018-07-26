using System.Collections.Generic;

namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModuleLoader
    {
        List<ICoreWikiModule> Load(string path);
        List<ICoreWikiModule> Load(params string[] paths);
    }
}