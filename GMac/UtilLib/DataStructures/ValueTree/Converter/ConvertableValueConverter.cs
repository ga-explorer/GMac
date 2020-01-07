using System;
using System.Globalization;

namespace UtilLib.DataStructures.ValueTree.Converter
{
    public abstract class ConvertableValueConverter<T> : IValueConverter<T> where T : IConvertible
    {
        public IFormatProvider ToStringFormatProvider { get; private set; }


        protected ConvertableValueConverter()
        {
            ToStringFormatProvider = null;
        }

        protected ConvertableValueConverter(IFormatProvider provider)
        {
            ToStringFormatProvider = provider;
        }


        public abstract T StringToValue(string s);

        public string ValueToString(T value)
        {
            return 
                value
                .ToString(
                    ReferenceEquals(ToStringFormatProvider, null) 
                    ? CultureInfo.InvariantCulture 
                    : ToStringFormatProvider
                    );
        }
    }
}
