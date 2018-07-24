using System;
using System.ComponentModel;

namespace CoreWiki.Extensibility.Common
{
	public class PreSubmitArticleEventArgs : CancelEventArgs
    {
        public PreSubmitArticleEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
        public string ModelErrorProperty { get; set; }
        public string ModelErrorMessage { get; set; }
    }

    public class ArticleSubmittedEventArgs : EventArgs
    {
        public ArticleSubmittedEventArgs(string topic, string content)
        {
            Topic = topic;
            Content = content;
        }

        public string Topic { get; set; }
        public string Content { get; set; }
    }

    public class CoreWikiModuleEvents
    {
        public Action<PreSubmitArticleEventArgs> PreSubmitArticle;
        public Action<ArticleSubmittedEventArgs> ArticleSubmitted;
    }

    public interface ICoreWikiModule
    {
        void Initialize(CoreWikiModuleEvents moduleEvents);
    }


    public class ExtensibilityModulesConfig
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public interface IExtensibilityManager
    {
        void InvokeModuleEvent<T>(Action<T> moduleEvent, T args);
        void InvokeCancelableModuleEvent<T>(Action<T> moduleEvent, T args);
    }

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
