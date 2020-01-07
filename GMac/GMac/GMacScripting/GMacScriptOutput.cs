using System;
using System.Collections.Generic;
using System.Drawing;
using GMac.GMacAST.Expressions;
using TextComposerLib.Text.Linear;

namespace GMac.GMacScripting
{
    public sealed class GMacScriptOutput
    {
        public LinearTextComposer Log { get; }

        public List<GMacScriptOutputItem> Items { get; }


        internal GMacScriptOutput()
        {
            Log = new LinearTextComposer();
            Items = new List<GMacScriptOutputItem>();
        }


        public void Clear()
        {
            Log.Clear();
            Items.Clear();
        }

        public GMacScriptOutputItem Store(string title, string description, string item)
        {
            var outputItem = new GMacScriptOutputItem(title, description, item);

            Items.Add(outputItem);

            return outputItem;
        }

        public GMacScriptOutputItem Store(string title, string item)
        {
            var outputItem = new GMacScriptOutputItem(title, String.Empty, item);

            Items.Add(outputItem);

            return outputItem;
        }

        public GMacScriptOutputItem Store(string title, string description, Image item)
        {
            var outputItem = new GMacScriptOutputItem(title, description, item);

            Items.Add(outputItem);

            return outputItem;
        }

        public GMacScriptOutputItem Store(string title, Image item)
        {
            var outputItem = new GMacScriptOutputItem(title, String.Empty, item);

            Items.Add(outputItem);

            return outputItem;
        }

        public GMacScriptOutputItem Store(string title, string description, AstValue item)
        {
            var outputItem = new GMacScriptOutputItem(title, description, item);

            Items.Add(outputItem);

            return outputItem;
        }

        public GMacScriptOutputItem Store(string title, AstValue item)
        {
            var outputItem = new GMacScriptOutputItem(title, String.Empty, item);

            Items.Add(outputItem);

            return outputItem;
        }
    }
}
