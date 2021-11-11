using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Threading;
using HallOfFame.Common.Interfaces;

namespace HallOfFame.Common
{
    /// <summary>
    /// Implementation of ISimplifiedLogger.
    /// </summary>
    public class SimpleFileLogger : ISimplifiedLogger
    {
        private readonly Semaphore _sem = new(1, 1);
        /// <summary>
        /// Save log into a file.
        /// </summary>
        /// <param name="filepath">Path to the file.</param>
        /// <param name="message">Message to log.</param>
        private async void SaveInFile(string filepath, string message)
        {
            _sem.WaitOne();

            await using (var streamWriter = new StreamWriter(filepath, true))
            {
                await streamWriter.WriteLineAsync(message);
            }

            _sem.Release();

        }
        /// <summary>
        /// Log an exception and some extra message if needed.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        /// <param name="extraMessage">Some extra message to log.</param>
        public void Log(Exception exception, params string[] extraMessage)
        {
            var directory = Directory.GetParent(Assembly.GetExecutingAssembly().Location)?.FullName;
            var date = DateTime.Now.ToShortDateString();
            var filePath = $"{directory}/logs/{date}.txt";
            var message = exception.ToString();
            message = extraMessage
                .Aggregate(message, 
                    (current, currentString) => current + $"\n{currentString}");


            var writingThread = new Thread(() => SaveInFile(filePath, message));
            writingThread.Start();
        }
    }
}
