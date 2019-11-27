using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoggerTester.NetCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MultiLogger Log;

        public MainWindow()
        {
            InitializeComponent();
            LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(MessageBoxLogger) });
            Log = LoggerFactory.GetNewLogger("WPFTester");
        }

        private void FireTestExButton_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = new Exception("This is a test of the exception message.");
            Log.Debug(ex, "This is a {0}", "test");
        }

        private void FireTestButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Debug("This is a {0}", "test");
        }
    }
}
