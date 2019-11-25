using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Logging
{
    public class MultiLogger : ILogger
    {
        /// <summary>
        /// Start with an empty name, so there is something there.
        /// </summary>
        private String name = "";
        private LogLevelEnum logLevel;

        public MultiLogger(LogLevelEnum level, String name, Type[] args)
        {
            if (String.IsNullOrEmpty(name)) name = "MultiLogger";
            LoggerFactory.SetLogLevel(level);
            foreach (Type logger in args)
            {
                LoggerFactory.AddToLoggers(logger);
            }
            
            
            //Info("Created logger with LogLevel " + LogLevel.ToString());
        }

        public MultiLogger(LogLevelEnum logLevel, String name)
        {
            LoggerFactory.SetLogLevel(logLevel);
            this.name = name;
        }

        public MultiLogger(String name) => this.name = name;

        public String Name { get => name; set => name = value; }

        private String AddNameToMessage(String message)
        {
            return name + ": " + message;
        }
               
        public ILogger GetILogger(String name)
        {
            return LoggerFactory.GetLoggerByName(name);
        }
        #region level methods
        public void Debug(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Debug,ex,message,args);
        }
        public void Debug(String message, params Object[] args)
        {
            Log(LogLevelEnum.Debug,message,args);
            
        }
        public void Error(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Error,ex,message,args);
        }
        public void Error(String message, params Object[] args)
        {
            Log(LogLevelEnum.Error,message,args);
        }
        public void Info(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Info,ex,message,args);
        }
        public void Info(String message, params Object[] args)
        {
            Log(LogLevelEnum.Info,message,args);
        }
        public void Verbose(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Verbose,ex,message,args);
        }
        public void Verbose(String message, params Object[] args)
        {
           Log(LogLevelEnum.Verbose,message,args);
        } 
        public void Warning(Exception ex, String message, params Object[] args)
        {
            Log(LogLevelEnum.Warning,ex,message,args);
        }
        public void Warning(String message, params Object[] args)
        {
            Log(LogLevelEnum.Warning,message,args);
        }
        #endregion
        public void Log(LogLevelEnum level, String message, params Object[] args)
        {
          LoggerFactory.Log(name,level,message,args);
        }
        
        public void Log(LogLevelEnum level, Exception ex, String message, params Object[] args)
        {
          LoggerFactory.Log(name,level,ex,message,args);
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