using System;
using System.Collections.Generic;
using System.Text;

namespace UtilLib.DataStructures.ValueTree
{
    public sealed class TextStack
    {
        private readonly List<string> _items = new List<string>();
        private readonly List<string> _lines = new List<string>();

        private int _topOfStack = -1;

        public string Separator;


        public TextStack(string separator)
        {
            Separator = separator;
        }


        public void Clear()
        {
            _items.Clear();
            _topOfStack = -1;
            _lines.Clear();
        }

        public void ClearLines()
        {
            _lines.Clear();
        }

        public void ClearItemsStack()
        {
            _items.Clear();
            _topOfStack = -1;
        }

        public void Push(string item)
        {
            _topOfStack = _topOfStack + 1;

            if (_items.Count > _topOfStack)
                _items[_topOfStack] = item;
            else
                _items.Add(item);
        }

        public string Pop()
        {
            if (_topOfStack <= -1) 
                throw new InvalidOperationException();

            var item = _items[_topOfStack];

            _topOfStack = _topOfStack - 1;

            return item;
        }

        public string AddLeaf(string item, bool addNewLine = true)
        {
            Push(item);

            var result = NewLine;

            Pop();

            if (addNewLine)
                _lines.Add(result);

            return result;
        }

        public string NewLine
        {
            get
            {
                if (_topOfStack < 0)
                    return "";

                var s = new StringBuilder();

                for (var i = 0; i <= _topOfStack; i++)
                    s.Append(_items[i]).Append(Separator);

                s.Length = s.Length - Separator.Length;

                return s.ToString();
            }
        }

        public string AddNewLine()
        {
            var result = ToString();

            _lines.Add(result);

            return result;
        }

        public IEnumerable<string> StackItems
        {
            get
            {
                for (var i = 0; i <= _topOfStack; i++)
                    yield return _items[i];
            }
        }

        public IEnumerable<string> Lines
        {
            get
            {
                return _lines;
            }
        }

        public override string ToString()
        {
            var s = new StringBuilder();

            foreach (var line in Lines)
                s.AppendLine(line);

            return s.ToString();
        }
    }
}
