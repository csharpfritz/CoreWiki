using CoreWiki.Extensibility.Common.Events;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace CoreWiki.Extensibility.Common
{
    public class ExtensibilityManager : ExtensibilityManagerBase, IExtensibilityManager
    {
        private const string ModulesPath = "CoreWikiModules";

        private readonly ICoreWikiModuleHost _coreWikiModuleHost;
        private readonly ILogger<ExtensibilityManager> _logger;

        public ExtensibilityManager(
            ICoreWikiModuleHost coreWikiModuleHost, 
            ICoreWikiModuleLoader moduleLoader,
            ILoggerFactory loggerFactory)
            : base(coreWikiModuleHost, moduleLoader)
        {
            _coreWikiModuleHost = coreWikiModuleHost;
            _logger = loggerFactory.CreateLogger<ExtensibilityManager>();
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before a new user is registered.
        /// </summary>
        /// <returns></returns>
        public PreRegisterUserEventArgs InvokePreRegisterUserEvent()
        {
            try
            {
                var args = new PreRegisterUserEventArgs();

                return InvokeCancelableModuleEvent(_coreWikiModuleHost.Events.PreRegisterUser, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
                return null;
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after a new user is registered.
        /// </summary>
        public void InvokePostRegisterUserEvent()
        {
            try
            {
                var args = new PostRegisterUserEventArgs();
                InvokeModuleEvent(_coreWikiModuleHost.Events.PostRegisterUser, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before an article is created.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        /// <returns></returns>
        public PreArticleCreateEventArgs InvokePreArticleCreateEvent(string topic, string content)
        {
            try
            {
                var args = new PreArticleCreateEventArgs(topic, content);

                return InvokeCancelableModuleEvent(_coreWikiModuleHost.Events.PreCreateArticle, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
                return null;
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after an article is created.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        public void InvokePostArticleCreateEvent(string topic, string content)
        {
            try
            {
                var args = new PostArticleCreateEventArgs(topic, content);
                InvokeModuleEvent(_coreWikiModuleHost.Events.PostCreateArticle, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before an article is edited.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        /// <returns></returns>
        public PreArticleEditEventArgs InvokePreArticleEditEvent(string topic, string content)
        {
            try
            {
                var args = new PreArticleEditEventArgs(topic, content);

                return InvokeCancelableModuleEvent(_coreWikiModuleHost.Events.PreEditArticle, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
                return null;
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after an article is edited.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        public void InvokePostArticleEditEvent(string topic, string content)
        {
            try
            {
                var args = new PostArticleEditEventArgs(topic, content);
                InvokeModuleEvent(_coreWikiModuleHost.Events.PostEditArticle, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before a new comment is created.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <returns></returns>
        public PreCommentCreateEventArgs InvokePreCommentCreateEvent(string content)
        {
            try
            {
                var args = new PreCommentCreateEventArgs(content);

                return InvokeCancelableModuleEvent(_coreWikiModuleHost.Events.PreCreateComment, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
                return null;
            }
        }

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after a new comment is created.
        /// </summary>
        /// <param name="content">The content of the new comment.</param>
        public void InvokePostCommentCreateEvent(string content)
        {
            try
            {
                var args = new PostCommentCreateEventArgs(content);
                InvokeModuleEvent(_coreWikiModuleHost.Events.PostCreateComment, args);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                // Do not re-throw exceptions from the modules, this will crash the application
                // Todo: unload module if it throws an exception?
            }
        }

        private void InvokeModuleEvent<T>(Action<T> moduleEvent, T args)
        {
            if (moduleEvent == null) throw new ArgumentNullException(nameof(moduleEvent));
            if (args == null) throw new ArgumentNullException(nameof(args));

            moduleEvent.Invoke(args);
        }

        private T InvokeCancelableModuleEvent<T>(Action<T> moduleEvent, T args)
        {
            if (moduleEvent == null) throw new ArgumentNullException(nameof(moduleEvent));
            if (args == null) throw new ArgumentNullException(nameof(args));

            var cancel = false;

            foreach (var d in moduleEvent.GetInvocationList())
            {
                var eventDelegate = d as Action<T>;

                if (eventDelegate == null) continue;
                if (cancel) break;

                eventDelegate(args);

                var eventArgs = args as CancelEventArgs;
                if (eventArgs != null)
                    cancel = eventArgs.Cancel;
            }

            return args;
        }

        protected internal override void OnRegisterCoreWikiModules(ICoreWikiModuleHost coreWikiModuleHost, ICoreWikiModuleLoader moduleLoader)
        {
            var rootModulesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var modulesPath = Path.Join(rootModulesPath, ModulesPath);

            var modules = moduleLoader.Load(rootModulesPath, modulesPath);
            foreach (var coreWikiModule in modules)
            {
                coreWikiModule.Initialize(coreWikiModuleHost);
            }
        }
    }
}