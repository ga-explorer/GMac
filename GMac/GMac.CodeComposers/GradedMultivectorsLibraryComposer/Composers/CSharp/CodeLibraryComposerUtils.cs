using System.Text;

namespace GMac.CodeComposers.GradedMultivectorsLibraryComposer.Composers.CSharp
{
    internal static class CodeLibraryComposerUtils
    {
        internal static string ScalarItem(this string name, object row, object column)
        {
            return
                new StringBuilder()
                .Append(name)
                .Append("[")
                .Append(row)
                .Append(", ")
                .Append(column)
                .Append("]")
                .ToString();
        }


    }
}
