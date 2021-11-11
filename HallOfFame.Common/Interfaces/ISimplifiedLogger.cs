using System;

namespace HallOfFame.Common.Interfaces
{
    /// <summary>
    /// Interface for saving logs into a file.
    /// </summary>
    public interface ISimplifiedLogger
    {
        /// <summary>
        /// Log an exception.
        /// </summary>
        /// <param name="exception">Exception.</param>
        /// <param name="extraMessage">Any additional strings to log.</param>
        public void Log(Exception exception, params string[] extraMessage);
    }
}
