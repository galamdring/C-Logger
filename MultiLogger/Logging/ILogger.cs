using System;

namespace Logging{
  public interface ILogger{
    string Name{ get;set;}
    ILogger GetILogger(string name);
    void SetLogLevel(LogLevelEnum logLevelEnum);
    void Log(LogLevelEnum level, string message, params object[] args);
    void Log(LogLevelEnum level, Exception ex, string message, params object[] args);
    void Debug(Exception ex, string message, params object[] args);
    void Debug(string message, params object[] args);
    void Verbose(Exception ex, string message, params object[] args);
    void Verbose(string message, params object[] args);
    void Info(Exception ex, string message, params object[] args);
    void Info(string message, params object[] args);
    void Warning(Exception ex, string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(Exception ex, string message, params object[] args);
    void Error(string message, params object[] args);
  }
}