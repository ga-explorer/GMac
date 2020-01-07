using System;

namespace GMac.GMacAPI.CodeGen
{
    public class GMacMacroCodeComposerDefaults 
    {
        public GMacCodeLibraryComposer LibraryComposer { get; }

        /// <summary>
        /// If true, no optimization by re-ordering of output variables computation is performed
        /// The default is false
        /// </summary>
        public bool FixOutputComputationsOrder { get; set; }

        /// <summary>
        /// If false, no code is actually generated from this macro code generator
        /// The default is true
        /// </summary>
        public bool AllowGenerateMacroCode { get; set; }

        /// <summary>
        /// This is executed before generating computation code. It can be used to add comments, declare temp 
        /// variables in the target code or any other similar purpose.
        /// </summary>
        public Func<GMacMacroCodeComposer, bool> ActionBeforeGenerateComputations { get; set; }

        /// <summary>
        /// This is executed after generating computation code. It can be used to add comments, destruct temp
        /// variables in the target code or or any other similar purpose.
        /// </summary>
        public Action<GMacMacroCodeComposer> ActionAfterGenerateComputations { get; set; }


        public GMacMacroCodeComposerDefaults(GMacCodeLibraryComposer libGen)
        {
            LibraryComposer = libGen;

            AllowGenerateMacroCode = true;

            ActionBeforeGenerateComputations = null;

            ActionAfterGenerateComputations = null;
        }

        /// <summary>
        /// Create an exact copy of this object
        /// </summary>
        /// <returns></returns>
        public GMacMacroCodeComposerDefaults Duplicate()
        {
            return new GMacMacroCodeComposerDefaults(LibraryComposer)
            {
                FixOutputComputationsOrder = FixOutputComputationsOrder,
                AllowGenerateMacroCode = AllowGenerateMacroCode,
                ActionBeforeGenerateComputations = ActionBeforeGenerateComputations,
                ActionAfterGenerateComputations = ActionAfterGenerateComputations
            };
        }
    }
}
