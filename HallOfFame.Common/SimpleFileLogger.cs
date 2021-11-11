using System;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Threading;
using HallOfFame.Common.Interfaces;

namespace HallOfFame.Common
{
    public class SimpleFileLogger : ISimplifiedLogger
    {
        private readonly Semaphore _sem = new(1, 1);

        private async void SaveInFile(string filepath, string message)
        {
            _sem.WaitOne();

            await using (var streamWriter = new StreamWriter(filepath, true))
            {
                await streamWriter.WriteLineAsync(message);
            }

            _sem.Release();

        }
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
