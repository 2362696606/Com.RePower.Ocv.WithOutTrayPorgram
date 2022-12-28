using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Org.BouncyCastle.Operators;

namespace Com.RePower.Ocv.Model
{
    public static class StringExtensions
    {
        public static decimal ToDecimalExt(this string str)
        {
            return decimal.Parse(str);
        }
        /// <summary>
        /// 为null或者长度为0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }


        /// <summary>
        /// 如果本向的字符串是null或者空格的话，就返回默认的defaultString,否则返回自身值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultString">默认值</param>
        /// <returns></returns>
        public static string DefaultString(this string str, string defaultString)
        {
            if (str.IsNullOrWhitespace())
            {
                return defaultString;
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 为null或者长度为0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhitespace(this string str)
        {
            return str == null || str.Trim().Length == 0;
        }
        /// <summary>
        /// 即不为null同时长度也不为0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullAndWhiteSpace(this string str)
        {
            return str != null && str.Trim().Length != 0;
        }
        public static bool IsRangeLength(this string str, int minLength, int maxLength)
        {
            if (str != null)
            {
                return str.Length >= minLength && str.Length <= maxLength;
            }
            return false;
        }
        public static string TrimEx(this string str, string defaultVal = null)
        {
            if (str == null) return defaultVal;
            return str.Trim();
        }
        public static string TrimRedisString(this string str)
        {
            if (str.IsNotNullAndWhiteSpace() && str.IsMatch(@"^\"".+\""$"))
            {
                return str.Trim('"');
            }
            return str;
        }
        public static string TrimIfEmpty(this string str, string defaultVal = null)
        {
            if (str == null) return defaultVal;
            str = str.Trim();
            if (str.Length == 0) return defaultVal;
            return str;
        }
        public static DateTime ToDateTime(this string str, DateTime defaultVal)
        {
            if (str.IsNullOrWhitespace())
            {
                return defaultVal;
            }
            DateTime i;
            if (DateTime.TryParse(str.Trim(), out i))
            {
                return i;
            }
            return defaultVal;
        }
        public static DateTime? ToDateTime(this string str, bool isThrowError = false)
        {
            DateTime i;
            if (DateTime.TryParse(str, out i))
            {
                return i;
            }
            else
            {
                if (isThrowError)
                {
                    throw new Exception($"字符串{str}转化为日期失败");
                }
            }
            return null;
        }
        /// <summary>
        /// 字符串转化成日期对象，有异常直接抛出
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeExt(this string str)
        {
            return DateTime.Parse(str);
        }
        public static bool ToBool(this string str, bool throwError = false, bool defaultValue = false)
        {
            if (bool.TryParse(str.Trim(), out var result))
            {
                return result;
            }
            else
            {
                if (throwError)
                {
                    throw new Exception($"字符串{str}不能被转化为bool值");
                }
            }
            return defaultValue;
        }

        public static string ReplaceReg(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }

        public static int ToInt(this string str, int defaultVal = 0, bool isThrowError = false)
        {
            int i = 0;
            if (int.TryParse(str, out i))
            {
                return i;
            }
            else
            {
                if (isThrowError)
                {
                    throw new Exception($"字符{str}转数字异常");
                }
            }
            return defaultVal;
        }
        public static long ToLong(this string str, bool isThrowError = false, long defaultVal = 0L)
        {
            if (!str.IsNullOrWhitespace())
            {
                long i;
                if (long.TryParse(str.Trim(), out i))
                {
                    return i;
                }
            }

            if (isThrowError)
            {
                throw new Exception($"字符串：{str}不能转化为long类型");
            }
            return defaultVal;
        }
        public static double ToDouble(this string str, double defaultVal = 0)
        {
            if (!str.IsNullOrWhitespace())
            {
                double i;
                if (double.TryParse(str.Trim(), out i))
                {
                    return i;
                }
            }
            return defaultVal; ;
        }

        /// <summary>
        /// 把字符串转化成float值
        /// </summary>
        /// <param name="str">要转化的字符串</param>
        /// <param name="throwError">当转化不成功时，是否抛出异常</param>
        /// <param name="defaultVal">不抛异常又转化失败时的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string str, bool throwError = false, float defaultVal = 0F)
        {
            if (!str.IsNullOrWhitespace())
            {
                if (float.TryParse(str.Trim(), out var i))
                {
                    return i;
                }
                else
                {
                    throw new Exception($"字符串{str}不能被转化成Float");
                }
            }
            return defaultVal;
        }
        public static T Deserialize<T>(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            return default(T);
        }
        /// <summary>
        /// 把压缩版的Json字符串转换成标准版的Json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToStandardJson(this string str)
        {
            if (str.IsNotNullAndWhiteSpace())
            {
                object deserialize = str.Deserialize<object>();
                return deserialize.ToJsonFormatted();
            }

            return str;
        }
        public static string ToMd5(this string str)
        {

            if (str != null)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
                return System.Text.Encoding.ASCII.GetString(result);
            }
            return null;
        }
        public static string FormatWith(this string str, params object[] args)
        {
            return String.Format(str, args);
        }
        public static string FormatWith2(this string str, params object[] arr)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            var n = arr.Length;
            var tempStr = str;
            for (int i = 0; i < n; i++)
            {
                var item = "{" + i + "}";
                if (tempStr.IndexOf(item, StringComparison.Ordinal) >= 0)
                {
                    tempStr = tempStr.Replace(item, arr[i].ToString());
                }
                else
                {
                    throw new Exception("替换字符串有空缺位！");
                }
            }
            return tempStr;
        }
        public static int GetMonthWeek(this DateTime dt)
        {
            int firstWeek = dt.AddDays(-dt.Day + 1).DayOfWeek.GetHashCode();//第一周之前的天数
            var week = (dt.Day + firstWeek - 1) / 7;
            return (int)Math.Floor((double)week) + 1;
        }
        public static T ToEnum<T>(this string str) where T : struct
        {
            T en;
            bool tryParse = Enum.TryParse(str, out en);
            if (!tryParse)
            {
                throw new Exception($"尝试转化enum类型时，字符串:{str},转换异常");
            }
            else
            {
                return en;
            }
        }
        /// <summary>
        /// 能根据枚举值上面的Description信息来返回相应的枚举值，如果找不到，就报错，不会返回默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToEnumByDescription<T>(this string str) where T : struct
        {
            Type type = typeof(T);   //获取类型
            string[] names = type.GetEnumNames();
            foreach (string name in names)
            {
                MemberInfo[] memberInfos = type.GetMember(name);
                foreach (MemberInfo memberInfo in memberInfos)
                {
                    var list = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if (list != null && list.Length > 0)
                    {
                        DescriptionAttribute item = list[0];
                        if (item.Description == str)
                        {
                            Enum.TryParse(name, out T en);
                            return en;
                        }
                    }
                }
            }
            throw new Exception($"不能根据枚举描述名称找到相应的枚举值：{str},需要转换的枚举类：{type.Name}");
        }
        public static T ToEnum<T>(this string str, T defaultVal) where T : struct
        {
            T en;
            if (Enum.TryParse(str, out en))
            {
                return en;
            }
            return defaultVal;
        }
        public static T? ToEnum<T>(this string str, T? defaultVal) where T : struct
        {
            T en;
            if (Enum.TryParse(str, out en))
            {
                return en;
            }
            return defaultVal;
        }
        public static bool InArrayString(this string str, string stringArray, string split = ",")
        {
            return str.InArrayString(stringArray, new string[] { split }, StringComparison.CurrentCulture);

        }
        public static bool InArrayString(this string str, string stringArray, string[] split, StringComparison stringComparison)
        {
            if (str.IsNullOrWhitespace() || stringArray.IsNullOrWhitespace())
            {
                return false;
            }
            return
                stringArray.Split(split, StringSplitOptions.RemoveEmptyEntries)
                    .Any(p => p.Equals(str, stringComparison));

        }
        /// <summary>
        /// 获得文件完整路径中的文件名称:如  D:\123.txt => 123.txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 获得文件的目录名称，D:\d1\123.txt => D:\d1
        /// </summary>
        /// <param name="path"></param>
        /// <param name="go">向上回溯的层次，如果是1，则不再向上取，默认为1</param>
        /// <returns></returns>
        public static string GetDirectoryName(this string path, int go = 1)
        {
            if (go > 1)
            {
                if (path.IsNullOrWhitespace())
                {
                    return string.Empty;
                }
                else if (Regex.IsMatch(path, @"^[a-zA-Z]:"))
                {
                    return Path.GetDirectoryName(path).GetDirectoryName(go - 1);
                }
                return string.Empty;
            }
            else
            {
                return Path.GetDirectoryName(path);
            }
        }
        /// <summary>
        /// 获取Path的文件后缀，D:\1.txt  => .txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileExtention(this string path)
        {
            return System.IO.Path.GetExtension(path);
        }
        /// <summary>
        ///   D:\Backup\my.txt  => my
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(this string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }
        /// <summary>
        /// 检测文件filepath是否存在:  如:  D:\1.txt
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool IsFileExists(this string filepath)
        {
            return File.Exists(filepath);
        }
        /// <summary>
        /// 检测文件夹directoryPath是否存在  如：  D:\dirname\    或者  D:\dirname
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static bool IsDirectoryExists(this string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        /// <summary>
        /// 获取一个文件夹路径的文件夹名称  如:  D:\123\  =>  123
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static string GetDirecrotyNameOfDirectoryPath(this string directoryPath)
        {
            return Path.GetFileName(directoryPath);
        }
        /// <summary>
        /// 判断日期格式是不是如：2019-01-02 11:12:13这种,或者2019-01-02T11:12:13
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static bool IsFullDateTimeString(this string strSource)
        {
            if (strSource == null) return false;
            var isMatch = Regex.IsMatch(strSource, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))( |T)(20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
            return isMatch;
        }

        /// <summary>
        /// 判断是不是一个标准的日期：如2015-03-04
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static bool IsDate(this string strSource)
        {
            return Regex.IsMatch(strSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        public static bool IsMatch(this string str, string pattern, RegexOptions option = RegexOptions.IgnoreCase)
        {
            if (str == null) return false;
            return Regex.IsMatch(str, pattern, option);
        }
        /// <summary>
        /// 字符串的正则表达式匹配，如果字符串为空，返回null,否则返回相应的结果
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static Match Match(this string str, string pattern, RegexOptions option = RegexOptions.None)
        {
            if (str.IsNull())
            {
                return null;
            }
            return Regex.Match(str, pattern, option);
        }

        public static MatchCollection Matches(this string str, string pattern, RegexOptions option = RegexOptions.IgnoreCase)
        {
            if (str.IsNullOrWhitespace())
            {
                return null;
            }

            return Regex.Matches(str, pattern, option);
        }
        //是否是整数，必须有一个数字，且必须是正整数或者0
        public static bool IsInt(this string StrSource)
        {
            if (StrSource.IsNull()) return false;
            return Regex.IsMatch(StrSource, @"^[0-9]$");
        }

        public static string TrimPath(this string path)
        {
            if (path.IsNotNullAndWhiteSpace())
            {
                try
                {
                    string replace = Regex.Replace(path, @"^\u202a", "");
                    return replace;
                }
                catch
                {

                    //ignore
                }
            }
            return path;
        }
        /// <summary>
        /// 使用正则表达式判断是否为日期 ,包括中文的日期，及数字的日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFullDateTime(this string str)
        {
            bool isDateTime = false;
            // yyyy/MM/dd  
            if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy-MM-dd   
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy.MM.dd   
            else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$"))
                isDateTime = true;
            // yyyy年MM月dd日  
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2})日)?$"))
                isDateTime = true;
            // yyyy年MM月dd日  
            else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}日)?$"))
                isDateTime = true;

            // yyyy年MM月dd日  
            else if (Regex.IsMatch(str, "^(零|〇|一|二|三|四|五|六|七|八|九|十){2,4}年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // yyyy年  
            //else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})年$"))  
            //    isDateTime = true;  

            // 农历1  
            else if (Regex.IsMatch(str, "^(甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
                isDateTime = true;
            // 农历2  
            else if (Regex.IsMatch(str, "^((甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月初(一|二|三|四|五|六|七|八|九|十)$"))
                isDateTime = true;

            // XX时XX分XX秒  
            else if (Regex.IsMatch(str, "^(?<hour>\\d{1,2})(时|点)(?<minute>\\d{1,2})分((?<second>\\d{1,2})秒)?$"))
                isDateTime = true;
            // XX时XX分XX秒  
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})(时|点)((零|一|二|三|四|五|六|七|八|九|十){1,3})分(((零|一|二|三|四|五|六|七|八|九|十){1,3})秒)?$"))
                isDateTime = true;
            // XX分XX秒  
            else if (Regex.IsMatch(str, "^(?<minute>\\d{1,2})分(?<second>\\d{1,2})秒$"))
                isDateTime = true;
            // XX分XX秒  
            else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})分((零|一|二|三|四|五|六|七|八|九|十){1,3})秒$"))
                isDateTime = true;

            // XX时  
            else if (Regex.IsMatch(str, "\\b(?<hour>\\d{1,2})(时|点钟)\\b"))
                isDateTime = true;
            else
                isDateTime = false;

            return isDateTime;
        }
        /// <summary>
        /// 连接两个路径,使用Path.Combine()
        /// </summary>
        /// <param name="str"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CombinePath(this string str, string path)
        {
            return Path.Combine(str, path);
        }
        /// <summary>
        /// 把字符串转化成唯一的long数字，因为long最大数字为`9223372036854775807`.length==19位，所以，字符串的长度可以会有限制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUniqueLongString(this string str)
        {
            long sum = 0;
            foreach (char t in str)
            {
                sum = (long)((16 * sum) ^ Convert.ToUInt32(t));
                var overflow = (byte)(sum / 4294967296);
                sum -= overflow * 4294967296;
                sum ^= overflow;
            }
            if (sum > 2147483647)
                sum -= 4294967296;
            else if (sum >= 32768 && sum <= 65535)
                sum -= 65536;
            else if (sum >= 128 && sum <= 255)
                sum -= 256;
            sum = Math.Abs(sum);
            return sum.ToString();
        }

        /// <summary>
        /// 生成随机字符串 
        /// </summary>
        /// <param name="randomString">随便的一个字符串都能触发，相当于种子</param>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，1=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        public static string GetRandomString(this string randomString, int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        public static string Add4(this string str)
        {
            return str + GetRandomString("", 4, true, true, true, false, "");
        }
        /// <summary>
        /// 把16进制字符串转化成byte[]
        /// 字符串示例：
        /// "03 04" => new byte[]{0x03,0x04} 或者 "0x03 0x04"=>new byte[]{0x03,0x04} 字符串也可如：“4d544f430100315b8e”这种
        /// </summary>
        /// <param name="byteString"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromString(this string byteString)
        {
            MatchCollection matches = Regex.Matches(byteString, @"(?<= ,，)?(0x|X)?([0-9a-fA-F]{2})", RegexOptions.IgnoreCase);
            List<byte> list = new List<byte>();
            foreach (Match match in matches)
            {
                list.Add(Convert.ToByte(match.Groups[2].Value, 16));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 把任何字符串转化成byte[]，如字符串Hello=>48 65 6C 6C 6F,默认是ASCII，其它方式为UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string str, string mode = "ASCII")
        {
            if (mode == "ASCII")
                return Encoding.ASCII.GetBytes(str);
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 获取类型T的带有Description的特性的方法名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns> 
        public static string GetMethodNameByDescription<T>(this string str) where T : class
        {
            Type type = typeof(T);
            List<MethodInfo> list = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
            MethodInfo method = list.FirstOrDefault(p =>
            {
                System.ComponentModel.DescriptionAttribute item = (System.ComponentModel.DescriptionAttribute)p.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute)).FirstOrDefault();
                if (item != null && item.Description == str)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
            if (method != null)
            {
                return method.Name;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 把16进制后的字符串变成byte数组,
        /// 如:0F 12 FF => byte[]数组:new byte[]{0x0F,0x12,pxFF}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes(this string str)
        {
            string[] strList = Regex.Split(str, @"\s+");
            byte[] array = strList.Select(HexStringToByte).ToArray();
            return array;
        }
        /// <summary>
        /// 把16进制的字符串转化为byte数组,只要是16进制的字符就可以，转化方式为每读取2个字符就转化一次，自动忽略空格
        /// "AA 00 01"=> new byte[]{0xAA,0x00,0x01}
        /// "AA0001"=> new byte[]{0xAA,0x00,0x01}
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ConvertToBytes2(this string str)
        {
            MatchCollection matches = Regex.Matches(str, @"\w{2}", RegexOptions.IgnoreCase);
            List<byte> list = new List<byte>();
            foreach (Match match in matches)
            {
                byte hexStringToByte = HexStringToByte(match.Value);
                list.Add(hexStringToByte);
            }

            return list.ToArray();
        }

        public static byte HexStringToByte(this string str)
        {
            return (byte)Convert.ToInt32(str, 16);
        }
        /// <summary>
        /// 从开头取字符串length长度的子串，如果不够，返回原字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TakeLength(this string str, int length)
        {
            if (str != null)
            {
                int len = str.Length;
                if (len > length)
                {
                    return str.Substring(0, length);
                }

                return str;
            }
            return null;
        }

        /// <summary>
        /// 合并两个字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="appendString"></param>
        /// <returns></returns>
        public static string App(this string str,string appendString)
        {
            return str + appendString;
        }
        /// <summary>
        /// 删除掉字符串中的注释，比如，以"abc   //注释内容",中的//后面的内容
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceComment(this string str)
        {
            return Regex.Replace(str, @"\/\/[^""]+\r?\n", "");
        }
        /// <summary>
        /// 首字母大写，其余字母小写：  OCV0   =>  Ocv0  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UpperFirstCase(this string str)
        {
            if (str.IsNotNullAndWhiteSpace())
            {
                return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
            }

            return str;
        }
        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetAbsolutePath(this string str)
        {
            return Path.GetFullPath(str);
        }
        /// <summary>
        /// 使用Path的Combine连接两个路径字符串
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string PathCombine(this  string path1,string path2)
        {
            return Path.Combine(path1, path2);
        }
    }
}
