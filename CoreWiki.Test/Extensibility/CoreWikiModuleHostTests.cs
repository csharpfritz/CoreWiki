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
    public class CoreWikiModuleHostTests
    {
        [Fact]
        public void PreRegisterUserEvent_RaisedEventWithExpectedEventArgs()
        {
            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PreRegisterUser += args => { receivedEvents.Add(args); };
            host.Events.PreRegisterUser(new PreRegisterUserEventArgs());

            Assert.Single(receivedEvents);
            Assert.IsType<PreRegisterUserEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PostRegisterUserEvent_RaisedEventWithExpectedEventArgs()
        {
            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PostRegisterUser += args => { receivedEvents.Add(args); };
            host.Events.PostRegisterUser(new PostRegisterUserEventArgs());

            Assert.Single(receivedEvents);
            Assert.IsType<PostRegisterUserEventArgs>(receivedEvents[0]);
        }

        [Fact]
        public void PreCreateArticleEvent_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PreCreateArticle += args => { receivedEvents.Add(args); };
            host.Events.PreCreateArticle(new PreArticleCreateEventArgs(topic, content));

            Assert.Single(receivedEvents);
            Assert.IsType<PreArticleCreateEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PreArticleCreateEventArgs;

            Assert.Equal(receivedEvent.Topic, topic);
            Assert.Equal(receivedEvent.Content, content);
        }

        [Fact]
        public void PostCreateArticleEvent_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PostCreateArticle += args => { receivedEvents.Add(args); };
            host.Events.PostCreateArticle(new PostArticleCreateEventArgs(topic, content));

            Assert.Single(receivedEvents);
            Assert.IsType<PostArticleCreateEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PostArticleCreateEventArgs;

            Assert.Equal(receivedEvent.Topic, topic);
            Assert.Equal(receivedEvent.Content, content);
        }

        [Fact]
        public void PreEditArticleEvent_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PreEditArticle += args => { receivedEvents.Add(args); };
            host.Events.PreEditArticle(new PreArticleEditEventArgs(topic, content));

            Assert.Single(receivedEvents);
            Assert.IsType<PreArticleEditEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PreArticleEditEventArgs;

            Assert.Equal(receivedEvent.Topic, topic);
            Assert.Equal(receivedEvent.Content, content);
        }

        [Fact]
        public void PostEditArticleEvent_RaisedEventWithExpectedEventArgs()
        {
            var topic = "topic";
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PostEditArticle += args => { receivedEvents.Add(args); };
            host.Events.PostEditArticle(new PostArticleEditEventArgs(topic, content));

            Assert.Single(receivedEvents);
            Assert.IsType<PostArticleEditEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PostArticleEditEventArgs;

            Assert.Equal(receivedEvent.Topic, topic);
            Assert.Equal(receivedEvent.Content, content);
        }

        [Fact]
        public void PreCreateCommentEvent_RaisedEventWithExpectedEventArgs()
        {
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PreCreateComment += args => { receivedEvents.Add(args); };
            host.Events.PreCreateComment(new PreCommentCreateEventArgs(content));

            Assert.Single(receivedEvents);
            Assert.IsType<PreCommentCreateEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PreCommentCreateEventArgs;

            Assert.Equal(receivedEvent.Content, content);
        }

        [Fact]
        public void PostCreateCommentEvent_RaisedEventWithExpectedEventArgs()
        {
            var content = "content";

            var receivedEvents = new List<EventArgs>();
            var host = GetCoreWikiModuleHost();

            host.Events.PostCreateComment += args => { receivedEvents.Add(args); };
            host.Events.PostCreateComment(new PostCommentCreateEventArgs(content));

            Assert.Single(receivedEvents);
            Assert.IsType<PostCommentCreateEventArgs>(receivedEvents[0]);

            var receivedEvent = receivedEvents[0] as PostCommentCreateEventArgs;

            Assert.Equal(receivedEvent.Content, content);
        }

        private CoreWikiModuleHost GetCoreWikiModuleHost()
        {
            var loggerMock = new MockLogger<EmailNotifier>();
            var optionsMock = new Mock<IOptionsSnapshot<AppSettings>>();
            optionsMock.Setup(x => x.Value).Returns(new AppSettings());

            var loggerFactoryMock = new Mock<MockLoggerFactory>();
            loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(loggerMock);

            var moduleEvents = new CoreWikiModuleEvents();
            return new CoreWikiModuleHost(moduleEvents, loggerFactoryMock.Object);
        }
    }
}
