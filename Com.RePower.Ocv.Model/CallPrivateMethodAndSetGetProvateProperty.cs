using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Com.RePower.Ocv.Model
{
    public static class CallPrivateMethodAndSetGetProvateProperty
    {
        #region 字段

        /// <summary>
        /// 获得对象字段的值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="fieldName">字段名称 string</param>
        /// <returns>字段值 T</returns>
        public static T GetFieldByName<T>(this object instance, string fieldName)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic| BindingFlags.Public;
            Type type = instance.GetType();
            FieldInfo field = GetFiledByName(type, fieldName, flag);
            if (field != null)
            {
                return (T)field.GetValue(instance);
            }

            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo.IsNotNull())
            {
                if (!(fieldInfo is null)) return (T) fieldInfo.GetValue(instance);
            }
            return default(T);
        }
        /// <summary>
        /// 获得对象字段的值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="fieldname">字段名称 string</param>
        /// <returns>字段值 T</returns>
        public static T GetValueByName<T>(this object instance, string fieldname)
        {
            T fieldByName = GetFieldByName<T>(instance, fieldname);
            T propertyByName = GetPropertyByName<T>(instance, fieldname);
            if (fieldByName.IsNotNull())
            {
                return fieldByName;
            }
            if (propertyByName.IsNotNull())
            {
                return propertyByName;
            }
            return default(T);
        }

        /// <summary>
        /// 设置字段的值,包括私有和公有字段
        /// </summary>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="fieldname">字段名称 string</param>
        /// <param name="value">字段新值 object</param>
        public static void SetFieldByName(this object instance, string fieldname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = instance.GetType();
            FieldInfo field = GetFiledByName(type, fieldname, flag);
            field.SetValue(instance, value);
        }

        private static FieldInfo GetFiledByName(Type type, string fieldname, BindingFlags flags)
        {
            if (type != null && !string.IsNullOrWhiteSpace(fieldname))
            {
                FieldInfo fieldInfo = type.GetField(fieldname, flags);
                if (fieldInfo == null)
                {
                    if (type.BaseType != typeof(object))
                    {
                        return GetFiledByName(type.BaseType, fieldname, flags);
                    }
                    return null;
                }
                return fieldInfo;
            }
            return null;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取对象属性的值，包括私有、公有的属性值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="propertyname">属性名称 string</param>
        /// <returns>私有字段值 T</returns>
        public static T GetPropertyByName<T>(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic| BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo field = GetProperty(type, propertyname, flag);
            return (T)field.GetValue(instance, null);
        }
        private static PropertyInfo GetProperty(Type type, string propertyname, BindingFlags flag)
        {
            if (type != null && !string.IsNullOrWhiteSpace(propertyname))
            {
                PropertyInfo propertyInfo = type.GetProperty(propertyname, flag);
                if (propertyInfo == null)
                {
                    if (type.BaseType != typeof(object))
                    {
                        return GetProperty(type.BaseType, propertyname, flag);
                    }
                }
                return propertyInfo;
            }
            return null;
        }

        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="propertyname">属性名称</param>
        /// <param name="value">属性新值</param>
        public static void SetPropertyByName(this object instance, string propertyname, object value)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo field = GetProperty(type, propertyname, flag);
            field.SetValue(instance, value, null);
        }
        /// <summary>
        /// 获取对象的所有实例属性，公共和非公共的，如果为对象为null,返回null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllProperties(this object obj)
        {
            if (obj != null)
            {
                BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                Type type = obj.GetType();
                return type.GetProperties(flag).ToList();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取类型的所有实例属性，公共和非公共的，如果为对象为null,返回null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetAllProperties(this Type type)
        {
            if (type != null)
            {
                BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                return type.GetProperties(flag).ToList();
            }

            return null;
        }

        #endregion


        #region 方法

        /// <summary>
        /// 调用私有方法或者公有方法
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="instance">扩展对象类型</param>
        /// <param name="name">方法名称 string</param>
        /// <param name="param">参数列表 object</param>
        /// <returns>调用方法返回值</returns>
        public static T CallInstanceMethod<T>(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic| BindingFlags.Public;
            Type type = instance.GetType();
            MethodInfo method = GetMethod(type, name, flag);
            if (method != null)
            {
                return (T) method.Invoke(instance, param);
            }
            else
            {
                throw new Exception("找不到类{0}中的方法：{1}".FormatWith(type.Name,name));
            }
        }
        /// <summary>
        /// 调用私有方法或者公有方法，并且无返回值
        /// </summary>
        /// <param name="instance">调用的对句实例</param>
        /// <param name="name">方法名称</param>
        /// <param name="param">参数列表</param>
        public static void CallInstanceMethodWithoutReturn(this object instance, string name, params object[] param)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = instance.GetType();
            MethodInfo method = GetMethod(type, name, flag);
            if (method != null)
            {
                method.Invoke(instance, param);
            }
            else
            {
                throw new Exception("找不到类{0}中的方法：{1}".FormatWith(type.Name, name));
            }
        }

        private static MethodInfo GetMethod(Type type, string typeName, BindingFlags flags)
        {
            if (type != null && typeName.IsNotNullAndWhiteSpace())
            {

                MethodInfo methodInfo = type.GetMethod(typeName, flags);
                if (methodInfo == null)
                {
                    Type baseType = type.BaseType;
                    if (baseType != null && baseType != typeof (object))
                    {
                        return GetMethod(baseType, typeName, flags);
                    }
                }
                return methodInfo;
            }
            return null;
        }

        /// <summary>
        /// 通过名称获取属性PropertyInfo
        /// </summary>
        /// <param name="instance">要搜索的对象</param>
        /// <param name="propertyName">属性的名称</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyObj(this object instance, string propertyName)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo property = GetProperty(type, propertyName, flag);
            return property;
        }
        #endregion
    }
}
