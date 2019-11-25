using System;

namespace Logging
{
    public class ConsoleLogger : ILogger
    {
        public String Name { get; set; }
        public LogLevelEnum LogLevel;

        public ConsoleLogger() => Name = "ConsoleLogger";

        public ConsoleLogger(String name) => Name = name;


        public ConsoleLogger(LogLevelEnum logLevel, String name)
        {
            this.Name = name;
        }
        public ILogger GetILogger(String name)
        {
            return new ConsoleLogger(name);
        }

        public void Log(LogLevelEnum level, String message, params Object[] args)
        {
            Console.WriteLine(String.Format("{0}:[{1}] {2} ", Name, level.ToString(), message), args);
        }

        public void Log(LogLevelEnum level, Exception ex, String message, params Object[] args)
        {
            Console.WriteLine(String.Format("{0}:[{1}] {2} ", Name, level.ToString(), ex.ToString() + '\n' + message), args);
        }

        public void SetLogLevel(LogLevelEnum logLevelEnum)
        {
            LogLevel = logLevelEnum;
        }
    }
}