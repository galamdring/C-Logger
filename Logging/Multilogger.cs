using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ContextProviderService.Logging
{
    public class MultiLogger : ILogger
    {
        static List<ILogger> Loggers = new List<ILogger>();
        static LogLevelEnum LogLevel = LogLevelEnum.Dev;
        static List<Type> LoggerTypes = new List<Type>();
        private String name = "";
        private LogLevelEnum logLevel;

        public MultiLogger(LogLevelEnum level, String name, Type[] args)
        {
            if (String.IsNullOrEmpty(name)) name = "MultiLogger";
            LogLevel = level;
            //I dont think this is right. We are adding it again when we create the instance. I'm hiding this one for now to see what happens.
            //LoggerTypes = ListFromArray(args);
            foreach (Type logger in args)
            {
                AddToLoggers(logger);
            }
            
            
            //Info("Created logger with LogLevel " + LogLevel.ToString());
        }

        public T GetLoggerByType<T>()
        {
            foreach(ILogger logger in Loggers)
            {
                if (logger.GetType().Equals(typeof(T))) return (T)logger;
            }

            return default(T);
        }

        public MultiLogger(LogLevelEnum logLevel, String name)
        {
            this.logLevel = logLevel;
            this.name = name;
        }

        public MultiLogger(String name) => this.name = name;

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

        private List<T> ListFromArray<T>(T[] array)
        {
            List<T> list = new List<T>();
            foreach(T item in array)
            {
                list.Add(item);
            }
            return list;
        }

        public String Name { get => name; set => name = value; }

        private String AddNameToMessage(String message)
        {
            return name + ": " + message;
        }

        public void Debug(Exception ex, String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Dev)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Debug(ex, AddNameToMessage(message), args);
                }
            }
        }
        public void Debug(String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Dev)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Debug(AddNameToMessage(message), args);
                }
            }
        }
        public void Error(Exception ex, String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Error)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Error(ex, AddNameToMessage(message), args);
                }
            }
        }
        public void Error(String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Error)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Error(AddNameToMessage(message), args);
                }
            }
        }
        public static ILogger GetLoggerByName(String name)
        {
            ILogger log = null;
            if (String.IsNullOrEmpty(name)) name = "MultiLogger";
            return GetNewLogger(name);
        }
        public static MultiLogger GetNewLogger(String name)
        {
            return new MultiLogger(name);
        }
        public ILogger GetICPSLogger(String name)
        {
            return MultiLogger.GetLoggerByName(name);
        }
        public void Info(Exception ex, String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Info)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Info(ex, AddNameToMessage(message), args);
                }
            }
        }
        public void Info(String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Info)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Info(AddNameToMessage(message), args);
                }
            }
        }
        public void Log(LogLevelEnum level, String message, params Object[] args)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(level, AddNameToMessage(message), args);
            }

        }
        public void Log(LogLevelEnum level, Exception ex, String message, params Object[] args)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.Log(level, ex, AddNameToMessage(message), args);
            }

        }
        public void SetLogLevel(LogLevelEnum logLevelEnum)
        {
            MultiLogger.LogLevel = logLevelEnum;
            Debug("Set the level to " + LogLevel.ToString());
        }
        
        public void Verbose(Exception ex, String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Verbose)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Verbose(ex, AddNameToMessage(message), args);
                }
            }
        }

        

        public void Verbose(String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Verbose)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Verbose(AddNameToMessage(message), args);
                }
            }
        } 
        public void Warning(Exception ex, String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Warning)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Warning(ex, AddNameToMessage(message), args);
                }
            }
        }
        public void Warning(String message, params Object[] args)
        {
            if (LogLevel <= LogLevelEnum.Warning)
            {
                foreach (ILogger logger in Loggers)
                {
                    logger.Warning(AddNameToMessage(message), args);
                }
            }
        }

         /// <summary>
        /// Removes all loggers of given type <T>.
        /// </summary>
        public static void RemoveLogger<T>()
        {
            foreach(ILogger logger in Loggers.ToList())
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