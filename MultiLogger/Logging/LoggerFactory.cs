using System;
using System.Collections.Generic;
using System.Linq;

namespace Logging
{
    public static class LoggerFactory
    {
        public static void CreateLoggers(LogLevelEnum level, Type[] args)
        {
            SetLogLevel(level);
            foreach (Type logger in args)
            {
                AddToLoggers(logger);
            }
        }
        /// <summary>
        /// This is all the loggers we want to log to.
        /// </summary>
        static List<ILogger> Loggers = new List<ILogger>();
        /// <summary>
        /// Initialize at the highest log level so we get all messages in the log until a different level is set.
        /// </summary>
        static LogLevelEnum LogLevel = LogLevelEnum.Debug;
        /// <summary>
        /// This is all the types of Loggers, so we don't duplicate them, and so we can recreate them when we need another with a new name.
        /// This is probably the wrong way to approach this, and we'll need to look at it again.
        /// </summary>
        static List<Type> LoggerTypes = new List<Type>();

        public static void SetLogLevel(LogLevelEnum logLevelEnum)
        {
            LogLevel = logLevelEnum;
            Log("LoggerFactory",LogLevelEnum.Debug,"Set the level to " + LogLevel.ToString(),null);
        }
        public static void AddToLoggers(Type loggerType)
        {
            if (LoggerTypes.Contains(loggerType))
            {
                //We already have this logger type, we can safely ignore being asked to add it again.
                return;
            }
            ILogger instance = (ILogger)Activator.CreateInstance(loggerType, LogLevel, "MultiLogger");
            Loggers.Add(instance);
            LoggerTypes.Add(loggerType);
        }
        /// <summary>
        /// Returns first Logger of Type T
        /// </summary>
        /// <typeparam name="T">Type of logger to search for</typeparam>
        /// <returns>First ILogger of type <typeparamref name="T"/></returns>
        public static T GetLoggerByType<T>()
        {
            foreach (ILogger logger in Loggers)
            {
                if (logger.GetType().Equals(typeof(T))) return (T)logger;
            }
            return default(T);
        }
        public static ILogger GetLoggerByName(String name)
        {
            if (String.IsNullOrEmpty(name)) name = "MultiLogger";
            return GetNewLogger(name);
        }
        public static MultiLogger GetNewLogger(String name)
        {
            return new MultiLogger(name);
        }

        public static void Log(String name, LogLevelEnum level, String message, params Object[] args)
        {
            if (LogLevel <= level)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Log(level, name + ":" + message, args);
                }
            }

        }
        public static void Log(string name, LogLevelEnum level, Exception ex, string message, params Object[] args)
        {
            if (LogLevel <= level)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Log(level, ex, name + ":" + message, args);
                }
            }
        }
        /// <summary>
        /// Removes all loggers of given type <T>.
        /// </summary>
        public static void RemoveLogger<T>()
        {
            foreach (ILogger logger in Loggers.ToList())
            {
                try
                {
                    var temp = (T)logger;
                    if (temp != null)
                    {
                        Loggers.Remove(logger);
                    }
                }
                catch
                {
                    continue;
                }

            }
        }
    }
}