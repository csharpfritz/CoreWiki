using CoreWiki.Core.Configuration;
using CoreWiki.Extensibility.Common;
using CoreWiki.Extensibility.Common.Events;
using CoreWiki.Notifications;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CoreWiki.Test.Extensibility
{
    public class ExtensibilityManagerTests
    {
        [Fact]
        public void PreRegisterUser_RaisedEventWithExpectedEventArgs()
        {
            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PreRegisterUser+= delegate(PreRegisterUserEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePreRegisterUserEvent();

            Assert.Single(receivedEvents);
            Assert.IsType<PreRegisterUserEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PostRegisterUser_RaisedEventWithExpectedEventArgs()
        {
            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PostRegisterUser+= delegate(PostRegisterUserEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePostRegisterUserEvent();

            Assert.Single(receivedEvents);
            Assert.IsType<PostRegisterUserEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PreCreateArticle_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PreCreateArticle += delegate(PreArticleCreateEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePreArticleCreateEvent(topic, content);

            Assert.Single(receivedEvents);
            Assert.IsType<PreArticleCreateEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PostCreateArticle_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PostCreateArticle += delegate(PostArticleCreateEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePostArticleCreateEvent(topic, content);

            Assert.Single(receivedEvents);
            Assert.IsType<PostArticleCreateEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PreEditArticle_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PreEditArticle += delegate(PreArticleEditEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePreArticleEditEvent(topic, content);

            Assert.Single(receivedEvents);
            Assert.IsType<PreArticleEditEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PostEditArticle_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PostEditArticle += delegate(PostArticleEditEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePostArticleEditEvent(topic, content);

            Assert.Single(receivedEvents);
            Assert.IsType<PostArticleEditEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PreCreateComment_RaisedEventWithExpectedEventArgs()
        {
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PreCreateComment += delegate(PreCommentCreateEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePreCommentCreateEvent(content);

            Assert.Single(receivedEvents);
            Assert.IsType<PreCommentCreateEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PostCreateComment_RaisedEventWithExpectedEventArgs()
        {
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var moduleEvents = new CoreWikiModuleEvents();
            moduleEvents.PostCreateComment += delegate(PostCommentCreateEventArgs args)
            {
                receivedEvents.Add(args);
            };

            var host = GetExtensibilityManager(moduleEvents);

            host.InvokePostCommentCreateEvent(content);

            Assert.Single(receivedEvents);
            Assert.IsType<PostCommentCreateEventArgs>(receivedEvents[0]);
        }

        private ExtensibilityManager GetExtensibilityManager(ICoreWikiModuleEvents moduleEvents)
        {
            var loggerMock = new MockLogger<EmailNotifier>();
            var optionsMock = new Mock<IOptionsSnapshot<AppSettings>>();
            optionsMock.Setup(x => x.Value).Returns(new AppSettings());

            var loggerFactoryMock = new Mock<MockLoggerFactory>();
            loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(loggerMock);

            var moduleLoaderMock = new Mock<CoreWikiModuleLoader>();

            var moduleHost = new CoreWikiModuleHost(moduleEvents, loggerFactoryMock.Object);
            return new ExtensibilityManager(moduleHost, moduleLoaderMock.Object, loggerFactoryMock.Object);
        }
    }
}
