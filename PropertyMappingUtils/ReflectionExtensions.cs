using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace PropertyMappingUtils
{
    public static class ReflectionExtensions
    {
        private static Type[] PrimativeTypes()
        {
            return new Type[] { 
                typeof(string), 
                typeof(char),
                typeof(byte),
                typeof(ushort),
                typeof(short),
                typeof(uint),
                typeof(int),
                typeof(ulong),
                typeof(long),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(DateTime),
                typeof(bool),
                typeof(Guid)
            };
        }
        public static string ToCodeString(this PropertyInfo propertyInfo, object instance)
        {
            var val = propertyInfo.GetValue(instance, null);
            if (val == null) { return "null"; }
            Type type = propertyInfo.BaseType();
            if (type == typeof(String)) 
            {
                return '"' + val.ToString().Replace("\\","\\\\") + '"';
            }
            if (type == typeof(Guid))
            {
                return "new Guid(\"" + val.ToString() + "\")";
            }
            if (type == typeof(Char))
            {
                return '\'' + val.ToString() + '\'';
            }
            if (type == typeof(DateTime))
            {
                var date = (DateTime)val;
                return string.Format("new DateTime({0},{1},{2},{3},{4},{5},{6})",
                    date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            }
            if (type == typeof(Boolean))
            {
                bool isTrue = (bool)val;
                return (isTrue) ? "true" : "false"; //because the ToString on a bool returns proper case!
            }
            return val.ToString();
        }
        public static IEnumerable<PropertyInfo> GetAssignablePrimativePropInfo(this Type type)
        {
            var primatives = PrimativeTypes();
            return type.GetProperties()
                        .Where(p => p.CanWrite && primatives.Contains(p.BaseType()))
                        .ToList();
        }
        public static bool IsNullable(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }
        public static Type BaseType(this PropertyInfo propertyInfo)
        {
            return (propertyInfo.IsNullable()) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;
        }
        public static void AssignFromString(this PropertyInfo propertyInfo, object instance, string text, bool truncateStringToAttrLen = false)
        {
            var conversion = propertyInfo.ReflectionParse(text, truncateStringToAttrLen);
            if (!conversion.ParseFailed)
            {
                propertyInfo.SetValue(instance, conversion.Obj, null);
            }
        }
        public static RegExPropertyMapMatch.MapItem ReflectionParse(this PropertyInfo propertyInfo, string text, bool truncateStringToAttrLen = false)
        {
            var tc = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
            var returnVal = new RegExPropertyMapMatch.MapItem { CapturedText = text };
            if (propertyInfo.PropertyType == typeof(String))
            {
                if (truncateStringToAttrLen)
                {
                    Object[] attribute = propertyInfo.GetCustomAttributes(typeof(StringLengthAttribute), true);

                    if (attribute.Length > 0)
                    {
                        StringLengthAttribute len = (StringLengthAttribute)attribute[0];
                        if (text.Length > len.MaximumLength)
                        {
                            returnVal.Obj = text.Substring(0, len.MaximumLength);
                        }
                    }
                }
                if (returnVal.Obj == null) { returnVal.Obj = text; }
            }
            else
            {
                try
                {
                    returnVal.Obj = tc.ConvertFromString(text);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is IndexOutOfRangeException)
                    {
                        returnVal.ParseFailed = true;
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
            return returnVal;
        }
        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
