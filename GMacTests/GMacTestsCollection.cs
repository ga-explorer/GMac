using System.Collections.Generic;
using TextComposerLib.Text.Linear;

namespace GMacTests
{
    public sealed class GMacTestsCollection : List<IGMacTest>, IGMacTest
    {
        public string Title { get; } = "";

        public string Execute()
        {
            var composer = new LinearComposer();

            foreach (var test in this)
                composer
                .AppendLineAtNewLine(test.Execute())
                .AppendLine();

            return composer.ToString();
        }
    }
}
