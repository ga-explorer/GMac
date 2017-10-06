namespace UtilLib.DataStructures.ValueTree.Converter
{
    public sealed class StringValueConverter : IValueConverter<string>
    {
        public string StringToValue(string s)
        {
            return s;
        }

        public string ValueToString(string value)
        {
            return value;
        }
    }
}
