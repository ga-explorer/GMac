namespace UtilLib.DataStructures.ValueTree.Converter
{
    public interface IValueConverter<T>
    {
        T StringToValue(string s);

        string ValueToString(T value);
    }
}
