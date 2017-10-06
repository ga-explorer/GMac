using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TextComposerLib.Text.Structured;

namespace TextComposerLib.Text
{
    public static class TextUtils
    {
        public static string Concatenate(params string[] items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item);

            return s.ToString();
        }
        
        public static string Concatenate<T>(params T[] items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item);

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<string> items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item);

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<StructuredTextItem> items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);

            return s.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> items)
        {
            var s = new StringBuilder();

            foreach (var item in items)
                s.Append(item);

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<string> items, string separator)
        {
            var s = new StringBuilder();

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<StructuredTextItem> items, string separator)
        {
            var s = new StringBuilder();

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);
            }

            return s.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> items, string separator)
        {
            var s = new StringBuilder();

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix)
        {
            var s = new StringBuilder();

            if (String.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            if (String.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<StructuredTextItem> items, string separator, string finalPrefix, string finalSuffix)
        {
            var s = new StringBuilder();

            if (String.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item.Prefix).Append(item.Text).Append(item.Suffix);
            }

            if (String.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix)
        {
            var s = new StringBuilder();

            if (String.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(item);
            }

            if (String.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }

        public static string Concatenate(this IEnumerable<string> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var s = new StringBuilder();

            if (String.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(itemPrefix).Append(item).Append(itemSuffix);
            }

            if (String.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }

        public static string Concatenate<T>(this IEnumerable<T> items, string separator, string finalPrefix, string finalSuffix, string itemPrefix, string itemSuffix)
        {
            var s = new StringBuilder();

            if (String.IsNullOrEmpty(finalPrefix) == false)
                s.Append(finalPrefix);

            var itemSeparator = separator ?? String.Empty;

            var flag = false;
            foreach (var item in items)
            {
                if (flag)
                    s.Append(itemSeparator);
                else
                    flag = true;

                s.Append(itemPrefix).Append(item).Append(itemSuffix);
            }

            if (String.IsNullOrEmpty(finalSuffix) == false)
                s.Append(finalSuffix);

            return s.ToString();
        }


        public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second)
        {
            var text = new StringBuilder();

            return
                first.Zip(
                    second,
                    (f, s) =>
                        text
                        .Clear()
                        .Append(f)
                        .Append(s)
                        .ToString()
                    );
        }

        public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second, string separator)
        {
            var text = new StringBuilder();

            return 
                first.Zip(
                    second, 
                    (f, s) => 
                        text
                        .Clear()
                        .Append(f)
                        .Append(separator)
                        .Append(s)
                        .ToString()
                    );
        }

        public static IEnumerable<string> JoinPairs(this IEnumerable<string> first, IEnumerable<string> second, string separator, string prefix, string suffix)
        {
            var text = new StringBuilder();

            return
                first.Zip(
                    second,
                    (f, s) =>
                        text
                        .Clear()
                        .Append(prefix)
                        .Append(f)
                        .Append(separator)
                        .Append(s)
                        .Append(suffix)
                        .ToString()
                    );
        }

    }
}
