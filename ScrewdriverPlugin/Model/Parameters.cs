using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс параметры.
    /// </summary>
    public class Parameters
    {
        //TODO: refactor

        /// <summary>
        /// Максимальное значение длины ручки.
        /// </summary>
        private const int HANDLE_LENGTH_MAX_VALUE = 150;

        /// <summary>
        /// Минимальное значение длины ручки.
        /// </summary>
        private const int HANDLE_LENGTH_MIN_VALUE = 45;

        /// <summary>
        /// Максимальное значение диаметра ручки.
        /// </summary>
        private const int HANDLE_WIDTH_MAX_VALUE = 42;

        /// <summary>
        /// Минимальное значение диаметра ручки.
        /// </summary>
        private const int HANDLE_WIDTH_MIN_VALUE = 7;

        /// <summary>
        /// Максимальное значение длины наконечника.
        /// </summary>
        private const int ROD_LENGTH_MAX_VALUE = 500;

        /// <summary>
        /// Минимальное значение длины наконечника.
        /// </summary>
        private const int ROD_LENGTH_MIN_VALUE = 45;

        /// <summary>
        /// Максимальное значение диаметра наконечника.
        /// </summary>
        private const int ROD_WIDTH_MAX_VALUE = 21;

        /// <summary>
        /// Минимальное значение диаметра наконечника.
        /// </summary>
        private const int ROD_WIDTH_MIN_VALUE = 3;

        /// <summary>
        /// Увеличивает в четыре раза.
        /// </summary>
        private const double FOURPLE = 4;

        /// <summary>
        /// Размер изменённой части наконечника.
        /// </summary>
        private const double ABS_DEVIATION = 5;

        /// <summary>
        /// Определяет разницу в диаметре.
        /// </summary>
        private const double DIAMETER_DEVIATION = 2;

        /// <summary>
        /// Определяет половину размера.
        /// </summary>
        private const double HALF = 2;

        /// <summary>
        /// Поле хранящее в себе словарь параметров.
        /// </summary>
        private Dictionary<ParameterType, Parameter> _parameters;

        /// <summary>
        /// Поле хранящее в себе тип ручки.
        /// </summary>
        private HandleType _handleType;

        /// <summary>
        /// Поле хранящее в себе тип наконечника.
        /// </summary>
        private RodType _rodType;

        /// <summary>
        /// Поле хранящее в себе информацию о наличии отверстия для возможности "повесить" отвёртку.
        /// </summary>
        private bool _isHoleExist;

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameters"/> class.
        /// </summary>
        public Parameters()
        {
            Parameter handleLength = new Parameter(HANDLE_LENGTH_MAX_VALUE, HANDLE_LENGTH_MIN_VALUE);
            handleLength.TypeOfParameter = ParameterType.HandleLength;
            Parameter handleWidth = new Parameter(HANDLE_WIDTH_MAX_VALUE, HANDLE_WIDTH_MIN_VALUE);
            handleWidth.TypeOfParameter = ParameterType.HandleWidth;
            Parameter rodLength = new Parameter(ROD_LENGTH_MAX_VALUE, ROD_LENGTH_MIN_VALUE);
            rodLength.TypeOfParameter = ParameterType.RodLength;
            Parameter rodWidth = new Parameter(ROD_WIDTH_MAX_VALUE, ROD_WIDTH_MIN_VALUE);
            rodWidth.TypeOfParameter = ParameterType.RodWidth;
            this.AllParameters = new Dictionary<ParameterType, Parameter>()
            {
                { ParameterType.HandleLength, handleLength },
                { ParameterType.HandleWidth, handleWidth },
                { ParameterType.RodLength, rodLength },
                { ParameterType.RodWidth, rodWidth },
            };
        }

        /// <summary>
        /// Gets для _parameters.
        /// </summary>
        public Dictionary<ParameterType, Parameter> AllParameters
        {
            get
            {
                return this._parameters;
            }

            private set
            {
                this._parameters = value;
            }
        }

        /// <summary>
        /// Gets or sets для поля _handleType (тип ручки).
        /// </summary>
        public HandleType ShapeOfHandle
        {
            get
            {
                return this._handleType;
            }

            set
            {
                this._handleType = value;
            }
        }

        /// <summary>
        /// Gets or sets для поля _rodType (тип наконечника).
        /// </summary>
        public RodType ShapeOfRod
        {
            get
            {
                return this._rodType;
            }

            set
            {
                this._rodType = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether для поля _isHoleExist (наличие отверстия).
        /// </summary>
        public bool IsHoleExist
        {
            get
            {
                return this._isHoleExist;
            }

            set
            {
                this._isHoleExist = value;
            }
        }

        /// <summary>
        /// Метод для добавления нового параметра в словарь.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="value">Значение.</param>
        public void SetParameter(ParameterType parameterType, int value)
        {
            try
            {
                this.AllParameters[parameterType].Value = value;
                this.ValidateParameters(this.AllParameters[parameterType]);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Валидация зависимых параметров.
        /// </summary>
        /// <exception cref="ArgumentException">Текст ошибки.</exception>
        private void ValidateParameters(Parameter parameter)
        {
            string message = string.Empty;
            switch (parameter.TypeOfParameter)
            {
                case ParameterType.HandleLength:
                {
                    Parameter handleWidth = this.AllParameters[ParameterType.HandleWidth];
                    Parameter rodLength = this.AllParameters[ParameterType.RodLength];
                    if (handleWidth.Value != 0 || rodLength.Value != 0)
                    {
                        if (handleWidth.Value != 0)
                        {
                            double maxValue = (handleWidth.Value + ABS_DEVIATION) * FOURPLE;
                            double minValue = (handleWidth.Value - ABS_DEVIATION) * FOURPLE;
                            if (parameter.Value > maxValue)
                            {
                                message += "Длина ручки более чем в 4 раза больше её диаметра" +
                                    ", уменьшите заданное значение минимум до "
                                    + maxValue.ToString() + '\n';
                            }
                            else if (parameter.Value < minValue)
                            {
                                message += "Длина ручки менее чем в 4 раза больше её диаметра" +
                                    ", увеличьте заданное значение минимум до "
                                    + minValue.ToString() + '\n';
                            }
                        }

                        if (rodLength.Value != 0 && rodLength.Value < parameter.Value)
                        {
                            message += "Длина ручки больше длины наконечника, " +
                                "уменьшите заданное значение минимум до " +
                                rodLength.Value.ToString() + '\n';
                        }
                    }

                    break;
                }

                case ParameterType.HandleWidth:
                {
                    Parameter handleLength = this.AllParameters[ParameterType.HandleLength];
                    Parameter rodWidth = this.AllParameters[ParameterType.RodWidth];
                    if (handleLength.Value != 0 || rodWidth.Value != 0)
                    {
                        if (handleLength.Value != 0)
                        {
                            double lowerQuarter = (double)((handleLength.Value / FOURPLE) -
                                ABS_DEVIATION);
                            double upperQuarter = (double)((handleLength.Value / FOURPLE) +
                                ABS_DEVIATION);
                            if (parameter.Value < lowerQuarter)
                            {
                                message += "Диаметр ручки меньше четверти длины ручки - 5 мм" +
                                    ", увеличьте заданное значение минимум до "
                                        + Math.Ceiling(lowerQuarter).ToString() + '\n';
                            }
                            else if (parameter.Value > upperQuarter)
                            {
                                message += "Диаметр ручки больше четверти длины ручки + 5 мм" +
                                    ", уменьшите заданное значение минимум до "
                                        + Math.Floor(upperQuarter).ToString() + '\n';
                            }
                        }

                        if (rodWidth.Value != 0)
                        {
                            double minValue = rodWidth.Value * HALF;
                            double maxValue = (rodWidth.Value + DIAMETER_DEVIATION) * HALF;
                            if (parameter.Value < minValue)
                            {
                                message += "Диаметр ручки не превышает диаметр наконечника " +
                                    "в 2 раза, увеличьте заданное значение минимум до "
                                   + minValue.ToString() + '\n';
                            }
                            else if (parameter.Value > maxValue)
                            {
                                message += "Диаметр ручки больше диаметра наконечника " +
                                    "более чем в 2 раза, уменьшите заданное значение минимум до "
                                   + maxValue.ToString() + '\n';
                            }
                        }
                    }

                    break;
                }

                case ParameterType.RodLength:
                {
                    Parameter handleLength = this.AllParameters[ParameterType.HandleLength];
                    if (handleLength.Value != 0 && parameter.Value < handleLength.Value)
                    {
                        message +=
                            "Длина наконечника меньше длины ручки, " +
                            "увеличьте заданное значение как минимум до "
                            + handleLength.Value.ToString() +
                            '\n';
                    }

                    break;
                }

                case ParameterType.RodWidth:
                {
                    Parameter handleWidth = this.AllParameters[ParameterType.HandleWidth];
                    if (handleWidth.Value != 0)
                    {
                        double upperHalfOfWidth = (double)handleWidth.Value;
                        upperHalfOfWidth = upperHalfOfWidth / HALF;
                        double lowerHalfOfWidth = (double)handleWidth.Value;
                        lowerHalfOfWidth = (lowerHalfOfWidth / HALF) - DIAMETER_DEVIATION;
                        if (parameter.Value < lowerHalfOfWidth)
                        {
                            message += "Диаметр наконечника меньше половины диаметра ручки, " +
                                "увеличьте заданное значение минимум до "
                                        + Math.Ceiling(lowerHalfOfWidth).ToString() + '\n';
                        }
                        else if (parameter.Value > upperHalfOfWidth)
                        {
                            message += "Диаметр наконечника больше половины диаметра ручки, " +
                                "уменьшите заданное значение минимум до "
                                        + Math.Floor(upperHalfOfWidth).ToString() + '\n';
                        }
                    }

                    break;
                }
            }

            if (message != string.Empty)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
