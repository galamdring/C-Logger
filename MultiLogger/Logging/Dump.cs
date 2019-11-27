using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Logging
{
    public static class Dump
    {
        private const int MAX_DEPTH = 10;
        private const int MAX_ITEMS_NUM = 20;

        public static string Content(object obj)
        {
            return Content(obj, 1);
        }

        private static string Content(object obj, int currentDepth)
        {
            try
            {
                if (currentDepth > MAX_DEPTH)
                {
                    return " ***";
                }

                if (obj == null)
                {
                    return "<null>";
                }

                if (obj.GetType().IsPrimitive || obj is DateTime || obj.GetType().IsEnum)
                {
                    return obj.ToString();
                }

                if (obj is string)
                {
                    return "\"" + obj + "\"";
                }

                if (obj is Exception)
                {
                    return "\"" + obj + "\"";
                }

                if (obj is IEnumerable)
                {
                    int i = 0;
                    var result = new StringBuilder();
                    foreach (var item in obj as IEnumerable)
                    {
                        i++;
                        if (i > MAX_ITEMS_NUM)
                        {
                            result.Append(" ***");
                            break;
                        }
                        result.AppendFormat(", {0}", Content(item, currentDepth + 1));
                    }
                    // Cutting trailing ', '
                    string valuesString = (result.Length > 1)
                        ? result.ToString(2, result.Length - 2)
                        : "empty";
                    return String.Format("{{{0}}}", valuesString);
                }
                else
                {
                    if (obj is MethodBase)
                    {
                        var objVar = obj as MethodBase;
                        return String.Format("[{0}: Name = {1}, DeclaringType = {2}]", obj.GetType().Name, objVar.Name, Content(objVar.DeclaringType, currentDepth + 1));
                    }

                    if (obj is Type)
                    {
                        return String.Format("[Type: FullName = {0}]", ((Type)obj).FullName);
                    }

                    int i = 0;
                    var result = new StringBuilder();
                    result.Append("[");
                    result.Append(obj.GetType().Name);
                    bool isEmpty = true;
                    int splitterPos = result.Length;
                    foreach (PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        i++;
                        if (i > MAX_ITEMS_NUM)
                        {
                            result.Append(" ***");
                            break;
                        }

                        // TODO: Support indexed properties?
                        if (prop.GetIndexParameters().Length > 0)
                            continue;
                        try
                        {
                            object propValue = prop.GetValue(obj, null);
                            if (propValue == null)
                                continue;
                            result.AppendFormat(", {0} = {1}", prop.Name, Content(propValue, currentDepth + 1));
                        }
                        catch (Exception)
                        {
                            // If we couldn't get property value
                            result.AppendFormat(", {0} = {1}", prop.Name, "<unknown>");
                            throw;
                        }

                        isEmpty = false;
                    }
                    if (!isEmpty)
                    {
                        // Replacing property list trailing ',' on ':'
                        result[splitterPos] = ':';
                    }
                    result.Append("]");
                    return result.ToString();
                }
            }
            catch
            {
                return "<failed>";
            }
        }
    }
}
