using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UtilLib
{
    /// <summary>
    /// Source: https://www.codeproject.com/Tips/876878/Calculating-Optimistic-Memory-Footprint-of-Managed
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Nice way to calculate the size of managed object!
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        internal class Size<TT>
        {
            private readonly TT _obj;
            private readonly HashSet<object> _references;
            private static readonly int PointerSize =
            Environment.Is64BitOperatingSystem ? sizeof(long) : sizeof(int);

            public Size(TT obj)
            {
                _obj = obj;
                _references = new HashSet<object>() { _obj };
            }

            public long GetSizeInBytes()
            {
                return GetSizeInBytes(_obj);
            }

            // The core functionality. Recurrently calls itself when an object appears to have fields 
            // until all fields have been  visited, or were "visited" (calculated) already.
            private long GetSizeInBytes<T>(T obj)
            {
                if (obj == null) return sizeof(int);
                var type = obj.GetType();

                if (type.IsPrimitive)
                {
                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.Boolean:
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                            return sizeof(byte);
                        case TypeCode.Char:
                            return sizeof(char);
                        case TypeCode.Single:
                            return sizeof(float);
                        case TypeCode.Double:
                            return sizeof(double);
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                            return sizeof(Int16);
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                            return sizeof(Int32);
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        default:
                            return sizeof(Int64);
                    }
                }
                else if (obj is decimal)
                {
                    return sizeof(decimal);
                }
                else if (obj is string)
                {
                    return sizeof(char) * obj.ToString().Length;
                }
                else if (type.IsEnum)
                {
                    return sizeof(int);
                }
                else if (type.IsArray)
                {
                    long size = PointerSize;
                    var casted = (IEnumerable)obj;
                    foreach (var item in casted)
                    {
                        size += this.GetSizeInBytes(item);
                    }
                    return size;
                }
                else if (obj is System.Reflection.Pointer)
                {
                    return PointerSize;
                }
                else
                {
                    long size = 0;
                    var t = type;
                    while (t != null)
                    {
                        size += PointerSize;
                        var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                        foreach (var field in fields)
                        {
                            var tempVal = field.GetValue(obj);
                            if (!_references.Contains(tempVal))
                            {
                                _references.Add(tempVal);
                                size += this.GetSizeInBytes(tempVal);
                            }
                        }
                        t = t.BaseType;
                    }
                    return size;
                }
            }
        }

        // The actual, exposed method:
        public static long SizeInBytes<T>(this T someObject)
        {
            var temp = new Size<T>(someObject);
            var tempSize = temp.GetSizeInBytes();
            return tempSize;
        }
    }
}
