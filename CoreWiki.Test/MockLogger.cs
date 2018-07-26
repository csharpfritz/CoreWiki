using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CoreWiki.Test
{
    public class MockLogger<T> : ILogger<T>
    {
        public MockLogger()
        {
        }

        /// <summary>
        /// Gets a list of messages logged by this logger.
        /// </summary>
        public List<string> LoggedMessages { get; } = new List<string>();

        /// <summary>
        /// Clears the list of logged messages.
        /// </summary>
        public void ClearLoggedMessages() => LoggedMessages.Clear();

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            LoggedMessages.Add(state.ToString());
        }

        /// <summary>
        /// Returns a value indicating wheter the logger is enabled for the given log level.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns>Always returns true.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Throws a NotImplementedException.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}