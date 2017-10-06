using System;
using System.Linq;
using TextComposerLib.Helpers;

namespace TextComposerLib.Text.Structured
{
    public sealed class PriorityQueueComposer<TPriority> : PriorityQueue<TPriority, StructuredTextItem>, IStructuredComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public PriorityQueueComposer()
        {
            Separator = String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }

        public PriorityQueueComposer(string separator)
        {
            Separator = separator ?? String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }


        public PriorityQueueComposer<TPriority> Enqueue(TPriority priority)
        {
            base.Enqueue(priority, this.ToTextItem(String.Empty));

            return this;
        }

        public PriorityQueueComposer<TPriority> Enqueue(TPriority priority, string item)
        {
            base.Enqueue(priority, this.ToTextItem(item));

            return this;
        }

        public PriorityQueueComposer<TPriority> Enqueue<T>(TPriority priority, T item)
        {
            base.Enqueue(priority, this.ToTextItem(item));

            return this;
        }


        public string Generate()
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(item => item.Value).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public string Generate(Func<StructuredTextItem, string> itemFunc)
        {
            var items = ReverseItems ? this.Reverse() : this;

            return items.Select(item => itemFunc(item.Value)).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}
