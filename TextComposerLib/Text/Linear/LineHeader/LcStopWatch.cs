using System.Diagnostics;

namespace TextComposerLib.Text.Linear.LineHeader
{
    public sealed class LcStopWatch : LcLineHeader
    {
        /// <summary>
        /// If true, each call to GetHeaderText() mathod results in a call to Reset() method
        /// </summary>
        public bool ResetOnRead { get; set; }

        public string FormatString { get; set; } //"hh:mm:ss.fff";

        private readonly Stopwatch _eventStopwatch = new Stopwatch();


        public LcStopWatch()
        {
            FormatString = "G";
            _eventStopwatch.Start();
        }

        public LcStopWatch(string formatString)
        {
            FormatString = formatString;
            _eventStopwatch.Start();
        }


        public override void Reset()
        {
            _eventStopwatch.Stop();
            _eventStopwatch.Reset();
            _eventStopwatch.Start();
        }

        public override string GetHeaderText()
        {
            var result =
                string.IsNullOrEmpty(FormatString) 
                ? _eventStopwatch.Elapsed.ToString() 
                : _eventStopwatch.Elapsed.ToString(FormatString);

            if (ResetOnRead)
                Reset();

            return result;
        }
    }
}
