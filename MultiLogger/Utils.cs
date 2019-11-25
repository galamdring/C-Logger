using System;
using System.Collections.Generic;

namespace MultiLogger
{
    public class Utils
    {
        private List<T> ListFromArray<T>(T[] array)
        {
            List<T> list = new List<T>();
            foreach (T item in array)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
