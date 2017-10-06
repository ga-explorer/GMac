using System;
using System.Collections.Generic;
using System.Linq;

namespace TextComposerLib.Text.Structured
{
    public sealed class ListComposer : List<StructuredTextItem>, IStructuredComposer
    {
        public string Separator { get; set; }

        public string ActiveItemPrefix { get; set; }

        public string ActiveItemSuffix { get; set; }

        public string FinalPrefix { get; set; }

        public string FinalSuffix { get; set; }

        public bool ReverseItems { get; set; }


        public ListComposer()
        {
            Separator = String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }

        public ListComposer(string separator)
        {
            Separator = separator ?? String.Empty;
            ActiveItemPrefix = String.Empty;
            ActiveItemSuffix = String.Empty;
            FinalPrefix = String.Empty;
            FinalSuffix = String.Empty;
        }


        public ListComposer Add()
        {
            base.Add(this.ToTextItem(String.Empty));

            return this;
        }

        public ListComposer Add(string item)
        {
            base.Add(this.ToTextItem(item));

            return this;
        }

        public ListComposer Add<T>(T item)
        {
            base.Add(this.ToTextItem(item));

            return this;
        }

        public ListComposer AddRange(IEnumerable<string> items)
        {
            base.AddRange(items.Select(this.ToTextItem));

            return this;
        }

        public ListComposer AddRange<T>(IEnumerable<T> items)
        {
            base.AddRange(items.Select(this.ToTextItem));

            return this;
        }

        public ListComposer AddRange(params string[] items)
        {
            base.AddRange(items.Select(this.ToTextItem));

            return this;
        }

        public ListComposer AddRange<T>(params T[] items)
        {
            base.AddRange(items.Select(this.ToTextItem));

            return this;
        }


        public string Generate()
        {
            var items =
                ReverseItems
                ? ((IEnumerable<StructuredTextItem>)this).Reverse()
                : this;

            return items.Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public string Generate(Func<StructuredTextItem, string> itemFunc)
        {
            var items =
                ReverseItems
                ? ((IEnumerable<StructuredTextItem>)this).Reverse()
                : this;

            return items.Select(itemFunc).Concatenate(Separator, FinalPrefix, FinalSuffix);
        }

        public override string ToString()
        {
            return Generate();
        }
    }
}
