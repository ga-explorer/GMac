using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Structured
{
    public sealed class StackComposer : Stack<StructuredTextItem>, IStructuredComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public StackComposer()
        {
            Separator = String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }

        public StackComposer(string separator)
        {
            Separator = separator ?? String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }


        public StackComposer Push()
        {
            base.Push(this.ToTextItem(String.Empty));

            return this;
        }

        public StackComposer Push(string item)
        {
            base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer Push<T>(T item)
        {
            base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer PushRange(IEnumerable<string> items)
        {
            foreach (var item in items)
                base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer PushRange<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
                base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer PushRange<T>(params T[] items)
        {
            foreach (var item in items)
                base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer PushRange(params string[] items)
        {
            foreach (var item in items)
                base.Push(this.ToTextItem(item));

            return this;
        }

        public StackComposer Pop(int n)
        {
            while (n > 0 && Count > 0)
            {
                Pop();
                n--;
            }

            return this;
        }


        public string Generate()
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public string Generate(Func<StructuredTextItem, string> itemFunc)
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(itemFunc).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}
