﻿using System;
using System.ComponentModel;

namespace CoreWiki.Extensibility.Common
{
    public class ExtensibilityManager : IExtensibilityManager
    {
        void IExtensibilityManager.InvokeModuleEvent<T>(Action<T> moduleEvent, T args)
        {
            if (moduleEvent != null)
                moduleEvent(args);
        }

        void IExtensibilityManager.InvokeCancelableModuleEvent<T>(Action<T> moduleEvent, T args)
        {
            if (moduleEvent != null)
            {
                bool cancel = false;
                Delegate[] invocationList = moduleEvent.GetInvocationList();
                foreach (Action<T> eventDelegate in invocationList)
                {
                    if (!cancel)
                    {
                        eventDelegate(args);
                        if (args is CancelEventArgs)
                            cancel = (args as CancelEventArgs).Cancel;
                    }
                    else
                        break;
                }
            }
        }
    }
}
