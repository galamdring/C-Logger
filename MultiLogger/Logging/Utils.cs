using System;
using System.Collections.Generic;

namespace Logging
{
    public class Utils
    {
        public static List<T> ListFromArray<T>(T[] array)
        {
            List<T> list = new List<T>();
            foreach (T item in array)
            {
                list.Add(item);
            }
            return list;
        }

        public static string Format(Exception e)
        {
            return e == null ? "Logged empty exception" : e.ToString();
        }

        public static string Format(string message, params object[] args)
        {
            try
            {
                object[] argsWithContent = new object[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    argsWithContent[i] = Dump.Content(args[i]);
                }
                return String.Format(message, argsWithContent);
            }
            catch (FormatException)
            {
                return "Logged message has invalid formatting, message: " + message;
            }
        }
    }
}
