using System;

namespace Logging
{
    public class ConsoleLogger : ICPSLogger
    {
        public String Name { get; set; }
        public LogLevelEnum LogLevel;

        public ConsoleLogger() => Name = "ConsoleLogger";

        public ConsoleLogger(String name) => Name = name;


        public ConsoleLogger(LogLevelEnum logLevel, String name)
        {
            this.Name = name;
        }

        public void Debug(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Dev, ex, message, args);
        }

        public void Debug(String message, params Object[] args)
        {
            Log(LogLevelEnum.Dev, message, args);
        }

        public void Error(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Error, ex, message, args);
        }

        public void Error(String message, params Object[] args)
        {
            Log(LogLevelEnum.Error, message, args);
        }

        public ILogger GetILogger(String name)
        {
            return new ConsoleLogger(name);
        }

        public void Info(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Info, ex, message, args);
        }

        public void Info(String message, params Object[] args)
        {
            Log(LogLevelEnum.Info, message, args);
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

        public void Verbose(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Verbose, ex, message, args);
        }

        public void Verbose(String message, params Object[] args)
        {
            Log(LogLevelEnum.Verbose, message, args);
        }

        public void Warning(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Warning, ex, message, args);
        }

        public void Warning(String message, params Object[] args)
        {
            Log(LogLevelEnum.Warning, message, args);
        }
    }
}