using System.Collections.Generic;
using TextComposerLib.Text.Linear;

namespace GMac.Benchmarks.Samples.Computations
{
    public sealed class GMacTestsCollection : List<IGMacSample>, IGMacSample
    {
        public string Title 
            => "";

        public string Description 
            => "";


        public string Execute()
        {
            var composer = new LinearTextComposer();

            foreach (var test in this)
                composer
                .AppendLineAtNewLine(test.Execute())
                .AppendLine();

            return composer.ToString();
        }
    }
}
