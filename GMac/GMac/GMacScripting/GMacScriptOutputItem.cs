using System.Drawing;
using System.Text;
using GMac.GMacAST.Expressions;

namespace GMac.GMacScripting
{
    public enum OutputItemType
    {
        Text, Image, Value
    }

    public sealed class GMacScriptOutputItem
    {
        public OutputItemType ItemType { get; }

        public object Item { get; }

        public string Title { get; }

        public string Description { get; }

        public string ItemAsText => Item as string;

        public Image ItemAsImage => Item as Image;

        public AstValue ItemAsValue => Item as AstValue;


        internal GMacScriptOutputItem(string title, string description, AstValue item)
        {
            ItemType = OutputItemType.Value;
            Item = item;
            Title = title;
            Description = description;
        }

        internal GMacScriptOutputItem(string title, string description, string item)
        {
            ItemType = OutputItemType.Text;
            Item = item;
            Title = title;
            Description = description;
        }

        internal GMacScriptOutputItem(string title, string description, Image item)
        {
            ItemType = OutputItemType.Image;
            Item = item;
            Title = title;
            Description = description;
        }


        public override string ToString()
        {
            var s = new StringBuilder();

            s.AppendLine(Title).AppendLine(Description);

            s.AppendLine(ItemType == OutputItemType.Image ? "<Image>" : Item.ToString());

            return s.ToString();
        }
    }
}
