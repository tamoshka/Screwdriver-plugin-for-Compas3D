using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrewdriverPlugin

{
    /// <summary>
    /// Класс параметр
    /// </summary>
    public class Parameter
    {
        private int _maxValue;
        private int _minValue;
        private int _value;

        /// <summary>
        /// Свойство для поля _maxValue (максимальное значение)
        /// </summary>
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

        /// <summary>
        /// Свойство для поля _minValue (минимальное значение)
        /// </summary>
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

        /// <summary>
        /// Свойство для поля _value (значение)
        /// </summary>
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

        /// <summary>
        /// Валидация вводимого значения _value в параметр
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void Validator()
        {
            if (Value<_minValue || Value>_maxValue)
            {
                throw new ArgumentException("Простая ошибка");
            }
        }
    }
}
