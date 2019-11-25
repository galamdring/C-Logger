using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Logging.Tests
{
    [TestClass()]
    public class ConsoleLoggerTests
    {
        [TestMethod()]
        public void ConsoleLoggerTest()
        {
            var cl = new ConsoleLogger();
            Assert.IsNotNull(cl);
        }

        [TestMethod()]
        public void GetILoggerTest()
        {
            var cl = new ConsoleLogger();
            Assert.AreEqual<ILogger>((ILogger)cl, cl.GetILogger("Whatever"));
        }

        [TestMethod()]
        public void LogTest()
        {
            var origOut = Console.Out;
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsoleLogger cl = new ConsoleLogger();
                cl.Log(LogLevelEnum.Info, "This is a {0}.", "test");
                String expected = String.Format("[Info] This is a test. {0}", Environment.NewLine);
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
                ConsoleLogger cl = new ConsoleLogger();
                Exception ex = new Exception("This is an exception test.");
                cl.Log(LogLevelEnum.Info,ex, "This is a {0}.", "test");
                String expected = String.Format("[Info] {1}{0}This is a test. {0}", Environment.NewLine,ex.ToString());
                Assert.AreEqual<string>(expected, sw.ToString());
            }
            Console.SetOut(origOut);
        }
    }
}