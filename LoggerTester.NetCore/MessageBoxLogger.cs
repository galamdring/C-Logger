using Logging;
using System;
using System.Threading.Tasks;

namespace LoggerTester.NetCore
{
    public class MessageBoxLogger : ILogger
    {
        public ILogger GetILogger(string name)
        {
            return this;
        }

        public void Log(LogLevelEnum level, string message, params object[] args)
        {
            //By firing the messagebox in a task, we don't block the ui of the main application. 
            //In some instances this isn't the best way to go, as we would want the user to have to interact
            //with the messagebox first.
            //Also, the messageboxes seem to be waiting on each other, so you only get the second after closing the first
            // and so on.
            Task.Factory.StartNew
                (
                    () =>
                    {
                        System.Windows.MessageBox.Show(
                            Utils.Format(message, args),
                            level.ToString(),
                            System.Windows.MessageBoxButton.OK,
                            System.Windows.MessageBoxImage.Information,
                            System.Windows.MessageBoxResult.OK,
                            System.Windows.MessageBoxOptions.DefaultDesktopOnly);
                    }
                );
        }

        public void Log(LogLevelEnum level, Exception ex, string message, params object[] args)
        {
            Log(level, Utils.Format(Utils.Format("{0}{1}{2}", Utils.Format(ex), Environment.NewLine, message), args));
        }
    }
}
