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
        /// <summary>
        /// Поле для максимального значения параметра
        /// </summary>
        private int _maxValue;

        /// <summary>
        /// Поле для минимального значения параметра
        /// </summary>
        private int _minValue;

        /// <summary>
        /// Поле для значения параметра
        /// </summary>
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
                try
                {
                    _value = value;
                    Validator();
                }
                catch (Exception ex) 
                {
                    throw new ArgumentException(ex.Message);
                }
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
