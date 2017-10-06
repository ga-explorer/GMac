using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Structured
{
    public sealed class QueueComposer : Queue<StructuredTextItem>, IStructuredComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public QueueComposer()
        {
            Separator = String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }

        public QueueComposer(string separator)
        {
            Separator = separator ?? String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }


        public QueueComposer Enqueue()
        {
            base.Enqueue(this.ToTextItem(String.Empty));

            return this;
        }

        public QueueComposer Enqueue(string item)
        {
            base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer Enqueue<T>(T item)
        {
            base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer EnqueueRange(IEnumerable<string> items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer EnqueueRange<T>(IEnumerable<T> items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer EnqueueRange(params string[] items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer EnqueueRange<T>(params T[] items)
        {
            foreach (var item in items)
                base.Enqueue(this.ToTextItem(item));

            return this;
        }

        public QueueComposer Dequeue(int n)
        {
            while (n > 0 && Count > 0)
            {
                base.Dequeue();
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
