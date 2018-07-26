using System;

namespace CoreWiki.Extensibility.Common
{
    public interface IExtensibilityManager
    {
        void InvokeModuleEvent<T>(Action<T> moduleEvent, T args);
        void InvokeCancelableModuleEvent<T>(Action<T> moduleEvent, T args);
    }
}
