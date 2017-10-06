namespace UtilLib
{
    public sealed class ComputedField<T>
    {
        public delegate T ComputationMethod();

        private bool _isSet;

        private T _value;
        
        private readonly ComputationMethod _computationFunction;


        public ComputedField(ComputationMethod compFunc)
        {
            _computationFunction = compFunc;
        }

        public void Reset()
        {
            _isSet = false;
            _value = default(T);
        }

        public T Value
        {
            get
            {
                if (_isSet)
                    return _value;

                _value = _computationFunction();
                _isSet = true;

                return _value;
            }
        }
    }
}
