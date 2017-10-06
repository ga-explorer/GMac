using System.Text;

namespace UtilLib.DataStructures.Tree.SimpleValueTree
{
    public sealed class LinearQualifiedTextListVisitor : ValueTreeNodeVisitor
    {
        private readonly StringBuilder _buffer;

        private bool _newLine;

        public string Separator;

        //public string NewLinePrefix;

        //public string NewLinePostfix;

        //public string ListPrefix;

        //public string ListPostfix;


        public LinearQualifiedTextListVisitor(string separator = ".")
        {
            _buffer = new StringBuilder();
            _newLine = true;

            Separator = separator;
        }


        public override void Visit<TValue>(ValueTreeLeafNode<TValue> node)
        {
            _buffer
                .Append("\"")
                .Append(node.Value)
                .AppendLine("\"");

            _newLine = true;
        }

        public override void Visit<TKey>(ValueTreeBranchNode<TKey> node)
        {
            _newLine = true;
        }

        public override void Visit<TKey, TNodeValue>(TKey key, ValueTreeLeafNode<TNodeValue> node)
        {
            if (_newLine)
                _newLine = false;

            else
                _buffer.Append(Separator);

            _buffer
                .Append(key)
                .Append(" = \"")
                .Append(node.Value)
                .AppendLine("\"");

            _newLine = true;
        }

        public override void Visit<TKey, TNodeKey>(TKey key, ValueTreeBranchNode<TNodeKey> node)
        {
            if (_newLine)
                _newLine = false;

            else
                _buffer.Append(Separator);

            _buffer.Append(key);

            if (node.HasChildNodes)
                return;

            _buffer.AppendLine();
            _newLine = true;
        }
    }
}
