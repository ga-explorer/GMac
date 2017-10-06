using System;

namespace UtilLib
{
    public static class GlobalIdGenerator
    {
        public static string StringIdTemplate = "#";
        public static string StringIdCountPlaceholder = "#";
        public static string StringIdCountFomatting = "";

        public static UInt64 CountValue { get; private set; }


        public static UInt64 GetNewCountId()
        {
            var value = CountValue;
            CountValue = CountValue + 1;
            return value;
        }

        public static string GetNewStringId(string prefix)
        {
            var newCount = GetNewCountId();

            var newCountStr = (StringIdCountFomatting == "") ? newCount.ToString() : newCount.ToString(StringIdCountFomatting);

            return prefix + newCountStr;
        }

        public static string GetNewStringId()
        {
            var newCount = GetNewCountId();

            var newCountStr = (StringIdCountFomatting == "") ? newCount.ToString() : newCount.ToString(StringIdCountFomatting);

            return StringIdTemplate.Replace(StringIdCountPlaceholder, newCountStr);
        }

        public static void SetStringIdTemplate(string template, string templatePlaceholder, string formatting)
        {
            StringIdTemplate = template;
            StringIdCountPlaceholder = templatePlaceholder;
            StringIdCountFomatting = formatting;
        }

    }
}
