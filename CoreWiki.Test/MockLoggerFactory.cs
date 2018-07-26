using Microsoft.Extensions.Logging;
using System;

namespace CoreWiki.Test
{
    public class MockLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Throws a NotImplementedException.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws a NotImplementedException.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public virtual ILogger CreateLogger(string categoryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws a NotImplementedException.
        /// </summary>
        /// <param name="provider"></param>
        public virtual void AddProvider(ILoggerProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Throws a NotImplementedException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual ILogger<T> CreateLogger<T>()
        {
            throw new NotImplementedException();
        }
    }
}