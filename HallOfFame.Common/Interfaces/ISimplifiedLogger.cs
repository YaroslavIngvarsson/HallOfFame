using System;

namespace HallOfFame.Common.Interfaces
{
    public interface ISimplifiedLogger
    {
        public void Log(Exception exception, params string[] extraMessage);
    }
}
