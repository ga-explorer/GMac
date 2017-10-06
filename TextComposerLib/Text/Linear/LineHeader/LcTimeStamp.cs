using System;
using System.Globalization;

namespace TextComposerLib.Text.Linear.LineHeader
{
    public sealed class LcTimeStamp : LcLineHeader
    {
        public string FormatString = "hh:mm:ss.fffffff";


        public LcTimeStamp()
        {
        }

        public LcTimeStamp(string formatString)
        {
            FormatString = formatString;
        }


        public override void Reset()
        {
        }

        public override string GetHeaderText()
        {
            return 
                string.IsNullOrEmpty(FormatString) 
                ? DateTime.Now.ToString(CultureInfo.InvariantCulture) 
                : DateTime.Now.ToString(FormatString);
        }
    }
}
