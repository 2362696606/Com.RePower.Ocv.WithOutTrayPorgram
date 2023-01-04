using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Com.RePower.Ocv.Model.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 返回对象格式化后的Json字符串
        /// </summary>
        /// <param name="obj1"></param>
        /// <returns></returns>
        public static string ToJsonFormatted(this object obj1)
        {
            string json = JsonConvert.SerializeObject(obj1);
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(json);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            string formatString;
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                formatString = textWriter.ToString();
            }
            else
            {
                formatString = json;
            }
            return formatString;
        }
        /// <summary>
        /// object对象转化为string，后强制转化为DateTime,此时要保证这个字符串一定能化为DateTime，不然会抛异常
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToObjDateTime(this object str)
        {
            string s = str.ToString();
            return DateTime.Parse(s);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        public static bool IsDefault<T>(this T obj)
        {
            T theDefault = default;
            if (obj == null && theDefault == null)
            {
                return true;
            }

            if (theDefault != null)
            {
                return theDefault.Equals(obj);
            }
            else
            {
                return obj.Equals(theDefault);
            }
        }
        /// <summary>
        /// 当前obj不为Null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
        public static T DoFunc<T>(this object obj, Func<T, T> action) where T : class
        {
            if (obj != null)
            {
                return action(obj as T);
            }
            return null;
        }
        public static void DoAction<T>(this object obj, Action<T> action) where T : class
        {
            if (obj != null)
            {
                action(obj as T);
            }
        }

        private static List<string> _basiceTypes = new List<string>() { "System.Boolean", "System.Byte", "System.SByte", "System.Char", "System.Decimal", "System.Double", "System.Single", "System.Int32", "System.UInt32", "System.Int64", "System.UInt64", "System.Object", "System.Int16", "System.UInt16", "System.String" };
        public static bool IsBasicType(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return _basiceTypes.Any(p => p == obj.GetType().FullName);
        }
        public static bool IsTypeBaseType(this Type t)
        {
            if (t == null)
            {
                return false;
            }
            return _basiceTypes.Any(p => p == t.FullName);
        }
        /// <summary>
        /// 是否是枚举类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEnumObject(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                Type memberInfo = obj.GetType().BaseType;
                return memberInfo != null && memberInfo.FullName == "System.Enum";
            }
        }
        /// <summary>
        /// 判断对象是不是数组
        /// 如：  int[] obj = new[] { 5, 6, 7 };
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsArrayObjectType(this object obj)
        {
            return obj is Array;
        }
        /// <summary>
        /// 是不是一个List
        ///例如： List<int/> l = new List<int>() { 1, 3, 57 };</int>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsListObjectType(this object obj)
        {
            return obj is IList;
        }
        /// <summary>
        /// 对象是不是一个Enumerable对象或者IEnumerable对象
        /// 如：IEnumerable<int> l2 = l.Select(p => p);</int>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsEnumerableObjectType(this object obj)
        {
            return obj is IEnumerable;
        }
        public static T To<T>(this object obj) where T : struct
        {
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }
            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }
                else
                {
                    throw new ArgumentException($"Enum type undefined '{obj}'.");
                }
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }
        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
    }
}
