namespace GMac.GMacAPI.CodeGen
{
    /// <summary>
    /// This abstract class can be used to implement a sub-process of string code generation using the main
    /// code library generator composnents
    /// </summary>
    public abstract class GMacCodeStringComposer : GMacCodePartComposer
    {

        protected GMacCodeStringComposer(GMacCodeLibraryComposer libGen)
            : base(libGen)
        {
        }


        public abstract string Generate();
    }
}
