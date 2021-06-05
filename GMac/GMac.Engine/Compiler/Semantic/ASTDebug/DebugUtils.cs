using System.Collections.Generic;
using System.Text;
using Irony.Parsing;

namespace GMac.Engine.Compiler.Semantic.ASTDebug
{
    public static class DebugUtils
    {
        //public static TraceLogComposer Trace = new TraceLogComposer();

        public static string DescribeParseTreeNode(ParseTreeNode node)
        {
            var s = new StringBuilder();

            var stackIndent = new Stack<int>();
            var stackNode = new Stack<ParseTreeNode>();

            stackIndent.Push(0);
            stackNode.Push(node);

            while (stackIndent.Count > 0)
            {
                var indent = stackIndent.Pop();
                var subNode = stackNode.Pop();

                if (indent > 0)
                    s.Append("".PadLeft(indent * 3, '-'));

                s.AppendLine(subNode.ToString());

                foreach (var childNode in subNode.ChildNodes)
                {
                    stackIndent.Push(indent + 1);
                    stackNode.Push(childNode);
                }
            }

            return s.ToString();
        }

    }
}
