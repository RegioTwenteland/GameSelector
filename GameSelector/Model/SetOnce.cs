using System;

namespace GameSelector.Model
{
    public class SetOnce<T>
    {
        private bool _set;
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                if (_set)
                {
                    throw new InvalidOperationException("This property can only be set once");
                }
                _set = true;
                _value = value;
            }
        }
    }
}
