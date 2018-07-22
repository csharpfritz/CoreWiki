using CoreWiki.Extensibility.Common.Events;

namespace CoreWiki.Extensibility.Common
{
    public interface IExtensibilityManager
    {
        /// <summary>
        /// Raises an event in all registered CoreWiki modules before a new user is registered.
        /// </summary>
        /// <returns></returns>
        PreRegisterUserEventArgs InvokePreRegisterUserEvent();

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after a new user is registered.
        /// </summary>
        void InvokePostRegisterUserEvent();

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before an article is created.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        /// <returns></returns>
        PreArticleCreateEventArgs InvokePreArticleCreateEvent(string topic, string content);

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after an article is created.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        void InvokePostArticleCreateEvent(string topic, string content);

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before an article is edited.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        /// <returns></returns>
        PreArticleEditEventArgs InvokePreArticleEditEvent(string topic, string content);

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after an article is edited.
        /// </summary>
        /// <param name="topic">The topic of the new article.</param>
        /// <param name="content">The content of the new article.</param>
        void InvokePostArticleEditEvent(string topic, string content);

        /// <summary>
        /// Raises an event in all registered CoreWiki modules before a new comment is created.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <returns></returns>
        PreCommentCreateEventArgs InvokePreCommentCreateEvent(string content);

        /// <summary>
        /// Raises an event in all registered CoreWiki modules after a new comment is created.
        /// </summary>
        /// <param name="content">The content of the new comment.</param>
        void InvokePostCommentCreateEvent(string content);
    }
}