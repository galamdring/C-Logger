using System;

namespace Logging{
  public interface ILogger{
    string Name{ get;set;}
    ILogger GetILogger(string name);
    void Log(LogLevelEnum level, string message, params object[] args);
    void Log(LogLevelEnum level, Exception ex, string message, params object[] args);
  }
}