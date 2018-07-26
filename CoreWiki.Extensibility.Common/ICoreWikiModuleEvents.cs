using System;
using CoreWiki.Extensibility.Common.Events;

namespace CoreWiki.Extensibility.Common
{
    public interface ICoreWikiModuleEvents
    {
        /// <summary>
        /// Raises an event in all registered CoreWikiModules before a new user is registered.
        /// </summary>
        Action<PreRegisterUserEventArgs> PreRegisterUser { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules after a new user is registered.
        /// </summary>
        Action<PostRegisterUserEventArgs> PostRegisterUser { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules before an article is created.
        /// </summary>
        Action<PreArticleCreateEventArgs> PreCreateArticle { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules after an article was created.
        /// </summary>
        Action<PostArticleCreateEventArgs> PostCreateArticle { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules before an article is created.
        /// </summary>
        Action<PreArticleEditEventArgs> PreEditArticle { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules after an article was edited.
        /// </summary>
        Action<PostArticleEditEventArgs> PostEditArticle { get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules before a comment is created.
        /// </summary>
        Action<PreCommentCreateEventArgs> PreCreateComment {get; set; }

        /// <summary>
        /// Raises an event in all registered CoreWikiModules after a comment was created.
        /// </summary>
        Action<PostCommentCreateEventArgs> PostCreateComment { get; set; }
    }
}