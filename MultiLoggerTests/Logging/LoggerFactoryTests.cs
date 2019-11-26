using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logging.Tests
{
    [TestClass()]
    public class LoggerFactoryTests
    {
        [TestMethod]
        public void CreateLoggersTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(ConsoleLogger) });
                LoggerFactory.Log("LoggerFactoryTest",LogLevelEnum.Info, "This is a {0}.", "test");
                String expected = String.Format("[Info] LoggerFactoryTest:This is a test. {0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
            
        }

        [TestMethod()]
        public void SetLogLevelTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                //Log once, increase the log level, then log again. Since we moved the log level higher than the indicated level,
                //we should only see the first log event.
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(ConsoleLogger) });
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                LoggerFactory.SetLogLevel(LogLevelEnum.Info);
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                String expected = String.Format("[Debug] LoggerFactoryTest:This is a test. {0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }

        [TestMethod()]
        public void AddToLoggersTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                //Log once, add the logger, then log again.
                //We should only see the second log event.
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { });
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                LoggerFactory.AddToLoggers(typeof(ConsoleLogger));
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                String expected = String.Format("[Debug] LoggerFactoryTest:This is a test. {0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }

        [TestMethod()]
        public void GetLoggerByTypeTest()
        {
            LoggerFactory.AddToLoggers(typeof(ConsoleLogger));
            Assert.IsInstanceOfType(LoggerFactory.GetLoggerByType<ConsoleLogger>(), typeof(ConsoleLogger));
        }

        [TestMethod()]
        public void GetLoggerByNameTest()
        {
            var expected = LoggerFactory.GetNewLogger("Testing");
            Assert.AreEqual(expected, LoggerFactory.GetLoggerByName("Testing"));
        }

        [TestMethod()]
        public void GetNewLoggerTest()
        {
            Assert.IsInstanceOfType(LoggerFactory.GetNewLogger("Testing"), typeof(MultiLogger));
        }

        [TestMethod()]
        public void LogTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(ConsoleLogger) });
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Info, "This is a {0}.", "test");
                String expected = String.Format("[Info] LoggerFactoryTest:This is a test. {0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }

        [TestMethod()]
        public void LogWithExTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(ConsoleLogger) });
                Exception ex = new Exception("This is a exception.");
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Info,ex, "This is a {0}.", "test");
                String expected = String.Format("[Info] {1}{0}LoggerFactoryTest:This is a test. {0}", Environment.NewLine, ex.ToString());
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }

        [TestMethod()]
        public void RemoveLoggerTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                //Log once, remove the logger, then log again.
                //We should only see the first log event.
                Console.SetOut(sw);
                LoggerFactory.CreateLoggers(LogLevelEnum.Debug, new Type[] { typeof(ConsoleLogger) });
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                LoggerFactory.RemoveLogger<ConsoleLogger>();
                LoggerFactory.Log("LoggerFactoryTest", LogLevelEnum.Debug, "This is a {0}.", "test");
                String expected = String.Format("[Debug] LoggerFactoryTest:This is a test. {0}", Environment.NewLine);
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }
    }
}