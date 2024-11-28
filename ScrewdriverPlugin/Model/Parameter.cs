using System;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс параметр.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Поле для максимального значения параметра.
        /// </summary>
        private int _maxValue;

        /// <summary>
        /// Поле для минимального значения параметра.
        /// </summary>
        private int _minValue;

        /// <summary>
        /// Поле для значения параметра.
        /// </summary>
        private int _value;

        /// <summary>
        /// Gets or sets для поля _maxValue (максимальное значение).
        /// </summary>
        public int MaxValue
        {
            get
            {
                return this._maxValue;
            }

            set
            {
                this._maxValue = value;
            }
        }

        /// <summary>
        /// Gets or sets для поля _minValue (минимальное значение).
        /// </summary>
        public int MinValue
        {
            get
            {
                return this._minValue;
            }

            set
            {
                this._minValue = value;
            }
        }

        /// <summary>
        /// Gets or sets для поля _value (значение).
        /// </summary>
        public int Value
        {
            get
            {
                return this._value;
            }

            set
            {
                try
                {
                    this._value = value;
                    this.Validator();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        /// <summary>
        /// Валидация вводимого значения _value в параметр.
        /// </summary>
        /// <exception cref="ArgumentException">Текст ошибки.</exception>
        private void Validator()
        {
            if (this.Value < this._minValue || this.Value > this._maxValue)
            {
                throw new ArgumentException("Простая ошибка");
            }
        }
    }
}
