using System.Collections.Generic;

namespace TextComposerLib.Code.SyntaxTree
{
    public sealed class TccTryCatchItem : SteSyntaxElement
    {
        public ISyntaxTreeElement CatchException { get; set; }

        public ISyntaxTreeElement CatchCode { get; set; }
    }

    public class SteTryCatch : SteSyntaxElement
    {
        public ISyntaxTreeElement TryCode { get; set; }

        public List<TccTryCatchItem> CatchItems { get; }

        public ISyntaxTreeElement FinallyCode { get; set; }


        public SteTryCatch()
        {
            CatchItems = new List<TccTryCatchItem>(); 
        }


        public SteTryCatch AddCatch(ISyntaxTreeElement catchException, ISyntaxTreeElement catchCode)
        {
            CatchItems.Add(
                new TccTryCatchItem()
                {
                    CatchException = catchException,
                    CatchCode = catchCode
                });

            return this;
        }

        public SteTryCatch AddCatch(string catchException, ISyntaxTreeElement catchCode)
        {
            CatchItems.Add(
                new TccTryCatchItem()
                {
                    CatchException = new SteFixedCode(catchException),
                    CatchCode = catchCode
                });

            return this;
        }

        public SteTryCatch AddCatch(string catchException, string catchCode)
        {
            CatchItems.Add(
                new TccTryCatchItem()
                {
                    CatchException = new SteFixedCode(catchException),
                    CatchCode = new SteFixedCode(catchCode)
                });

            return this;
        }
    }
}
