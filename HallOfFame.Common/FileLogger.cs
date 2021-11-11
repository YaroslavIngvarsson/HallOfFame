using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.IO;
using System.Reflection;

namespace HallOfFame.Common
{
    /// <summary>
    /// Implementation of ILogger for saving logs in a file.
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly Semaphore _semaphore = new(1, 1);
        /// <summary>
        /// Empty implementation.
        /// </summary>
        /// <typeparam name="TState">The entry to be written.</typeparam>
        /// <param name="state">The entry to be written.</param>
        /// <returns>Always null.</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        /// <summary>
        /// Enables the logger.
        /// </summary>
        /// <param name="logLevel">Level of logging.</param>
        /// <returns>True if log level is not none.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }
        /// <summary>
        /// Implementation of logging in file.
        /// </summary>
        /// <typeparam name="TState">The entry to be written.</typeparam>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="state">The entry to be written.</param>
        /// <param name="exception">The exception related to this entry.</param>
        /// <param name="formatter">Function to create a String message of the state and exception.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var directory = Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName;
            var date = DateTime.Now.ToShortDateString();
            var folderPath = $"{directory}/logs/";
            var filePath = $"{folderPath}/{date}.txt";
            var logRecord = "";
            if (exception is not null)
                logRecord = exception.ToString();

            var writingThread = new Thread(() =>
            {
                try
                {
                    _semaphore.WaitOne();

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var streamWriter = new StreamWriter(filePath, true))
                    {
                        streamWriter.WriteLineAsync(logRecord);
                    }
                }
                finally
                {
                    _semaphore.Release();
                }

            });
            writingThread.Start();
        }
    }
}
