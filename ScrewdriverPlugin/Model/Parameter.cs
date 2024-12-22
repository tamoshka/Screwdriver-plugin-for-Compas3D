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
        /// Поле для значения типа параметра.
        /// </summary>
        private ParameterType _typeOfParameter;

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
        /// Gets or sets для поля _typeOfParameter (значение).
        /// </summary>
        public ParameterType TypeOfParameter
        {
            get
            {
                return this._typeOfParameter;
            }

            set
            {
                this._typeOfParameter = value;
                this.DefineMinMax();
            }
        }

        /// <summary>
        /// Функция определяющая _maxValue и _minValue для parameter.
        /// </summary>
        private void DefineMinMax()
        {
            switch (this._typeOfParameter)
            {
                case ParameterType.HandleLength:
                    this.MinValue = 45;
                    this.MaxValue = 150;
                    break;
                case ParameterType.HandleWidth:
                    this.MinValue = 7;
                    this.MaxValue = 42;
                    break;
                case ParameterType.RodLength:
                    this.MinValue = 45;
                    this.MaxValue = 500;
                    break;
                case ParameterType.RodWidth:
                    this.MinValue = 3;
                    this.MaxValue = 21;
                    break;
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
