using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewdriverPlugin

{
    public class Parameter
    {
        private int _maxValue;
        private int _minValue;
        private int _value;
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
            }
        }

        public int MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
            }
        }

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                Validator();
            }
        }

        private void Validator()
        {
            if (Value<_minValue || Value>_maxValue)
            {
                throw new ArgumentException("Простая ошибка");
            }
        }
    }
}
