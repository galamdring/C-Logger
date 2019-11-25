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

        public MultiLogger(String name) => this.name = name;

        public String Name { get => name; set => name = value; }
               
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
        
    }
}