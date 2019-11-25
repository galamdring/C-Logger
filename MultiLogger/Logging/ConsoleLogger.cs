using System;

namespace Logging
{
    public class ConsoleLogger : ILogger
    { 
        public ConsoleLogger() { }
        public ILogger GetILogger(String name)
        {
            return this;
        }

        public void Log(LogLevelEnum level, String message, params Object[] args)
        {
            Console.WriteLine(String.Format("[{0}] {1} ", level.ToString(), message), args);
        }

        public void Log(LogLevelEnum level, Exception ex, String message, params Object[] args)
        {
            Console.WriteLine(String.Format("[{0}] {1} ", level.ToString(), ex.ToString() + Environment.NewLine + message), args);
        }
    }
}