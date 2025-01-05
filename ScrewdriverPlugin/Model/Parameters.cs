using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс параметры.
    /// </summary>
    public class Parameters
    {
        //TODO: refactor

        /// <summary>
        /// Увеличивает в четыре раза.
        /// </summary>
        private const double FOURPLE = 4;

        /// <summary>
        /// Размер изменённой части наконечника.
        /// </summary>
        private const double ABSDEVIATION = 5;

        /// <summary>
        /// Определяет разницу в диаметре.
        /// </summary>
        private const double DIAMETERDEVIATION = 2;

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
            this._parameters = new Dictionary<ParameterType, Parameter>()
            {
                { ParameterType.HandleLength, new Parameter(150, 45) },
                { ParameterType.HandleWidth, new Parameter(42, 7) },
                { ParameterType.RodLength, new Parameter(500, 45) },
                { ParameterType.RodWidth, new Parameter(21, 3) },
            };
        }

        /// <summary>
        /// Gets or sets для _parameters.
        /// </summary>
        public Dictionary<ParameterType, Parameter> AllParameters
        {
            get
            {
                return this._parameters;
            }

            set
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
                this.AllParameters.TryGetValue(parameterType, out Parameter parameter);
                parameter.Value = value;
                this.AllParameters.Remove(parameterType);
                this.AllParameters.Add(parameterType, parameter);
                this.ValidateParameters(parameter);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Функция определяющая _maxValue и _minValue для parameter.
        /// </summary>
        /// <param name="parameter">Параметр.</param>
        public void DefineMinMax(Parameter parameter)
        {
            switch (parameter.TypeOfParameter)
            {
                case ParameterType.HandleLength:
                {
                    parameter.MinValue = 45;
                    parameter.MaxValue = 150;
                    break;
                }

                case ParameterType.HandleWidth:
                {
                    parameter.MinValue = 7;
                    parameter.MaxValue = 42;
                    break;
                }

                case ParameterType.RodLength:
                {
                    parameter.MinValue = 45;
                    parameter.MaxValue = 500;
                    break;
                }

                case ParameterType.RodWidth:
                {
                    parameter.MinValue = 3;
                    parameter.MaxValue = 21;
                    break;
                }
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
                    if ((this.AllParameters.TryGetValue(
                        ParameterType.HandleWidth,
                        out Parameter chainedParameterFirst) && chainedParameterFirst.Value != 0) ||
                        (this.AllParameters.TryGetValue(
                        ParameterType.RodLength,
                        out Parameter chainedParameterSecond) && chainedParameterSecond.Value != 0))
                    {
                        if (this.AllParameters.TryGetValue(
                                ParameterType.HandleWidth,
                                out chainedParameterFirst) &&
                                chainedParameterFirst.Value != 0)
                        {
                            double maxValue = (chainedParameterFirst.Value + ABSDEVIATION) * FOURPLE;
                            double minValue = (chainedParameterFirst.Value - ABSDEVIATION) * FOURPLE;
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

                        if (this.AllParameters.TryGetValue(
                                ParameterType.RodLength,
                                out chainedParameterSecond) == true &&
                            (chainedParameterSecond.Value < parameter.Value) == true && chainedParameterSecond.Value != 0)
                        {
                            message += "Длина ручки больше длины наконечника, " +
                                "уменьшите заданное значение минимум до "
                            + chainedParameterSecond.Value.ToString() + '\n';
                        }
                    }

                    break;
                }

                case ParameterType.HandleWidth:
                {
                    if ((this.AllParameters.TryGetValue(
                        ParameterType.HandleLength,
                        out Parameter chainedParameterFirst) == true && chainedParameterFirst.Value != 0) ||
                        ((this.AllParameters.TryGetValue(
                        ParameterType.RodWidth,
                        out Parameter chainedParameterSecond) == true) && chainedParameterSecond.Value != 0))
                        {
                        if ((this.AllParameters.TryGetValue(
                                ParameterType.HandleLength,
                                out chainedParameterFirst) == true) && chainedParameterFirst.Value != 0)
                            {
                            double lowerQuarter = (double)((chainedParameterFirst.Value / FOURPLE) - ABSDEVIATION);
                            double upperQuarter = (double)((chainedParameterFirst.Value / FOURPLE) + ABSDEVIATION);
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

                        if ((this.AllParameters.TryGetValue(
                                ParameterType.RodWidth,
                                out chainedParameterSecond) == true) && chainedParameterSecond.Value != 0)
                            {
                            double minValue = chainedParameterSecond.Value * HALF;
                            double maxValue = (chainedParameterSecond.Value + DIAMETERDEVIATION) * HALF;
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
                    if (this.AllParameters.TryGetValue(
                        ParameterType.HandleLength,
                        out Parameter chainedParameterFirst) &&
                        chainedParameterFirst.Value != 0)
                    {
                        if (this.AllParameters.TryGetValue(
                                ParameterType.HandleLength,
                                out chainedParameterFirst) == true &&
                            parameter.Value < chainedParameterFirst.Value)
                        {
                            message += "Длина наконечника меньше длины ручки, " +
                                "увеличьте заданное значение как минимум до "
                            + chainedParameterFirst.Value.ToString() + '\n';
                        }
                    }

                    break;
                }

                case ParameterType.RodWidth:
                {
                    if (this.AllParameters.TryGetValue(
                        ParameterType.HandleWidth,
                        out Parameter chainedParameterThird) &&
                        chainedParameterThird.Value != 0)
                    {
                        double upperHalfOfWidth = (double)chainedParameterThird.Value;
                        upperHalfOfWidth = upperHalfOfWidth / HALF;
                        double lowerHalfOfWidth = (double)chainedParameterThird.Value;
                        lowerHalfOfWidth = (lowerHalfOfWidth / HALF) - DIAMETERDEVIATION;
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
