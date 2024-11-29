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
        /// <summary>
        /// Поле хранящее в себе текущий параметр.
        /// </summary>
        private Dictionary<ParameterType, Parameter> _parameter;

        /// <summary>
        /// Поле хранящее в себе словарь всех параметров.
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
        /// Метод для добавления нового параметра в словарь.
        /// </summary>
        /// <param name="parameterType">Тип добавляемого параметра.</param>
        /// <param name="parameter">Параметр.</param>
        public void SetParameter(ParameterType parameterType, Parameter parameter)
        {
            try
            {
                this._parameter = new Dictionary<ParameterType, Parameter>()
                {
                    { parameterType, parameter },
                };
                this.AllParameters.Remove(parameterType);
                this.AllParameters.Add(parameterType, parameter);
                this.ValidateParameters();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Валидация зависимых параметров.
        /// </summary>
        /// <exception cref="ArgumentException">Текст ошибки.</exception>
        private void ValidateParameters()
        {
            string exception = string.Empty;
            ParameterType parameterType = this._parameter.ElementAt(0).Key;
            Parameter parameter = this._parameter.ElementAt(0).Value;
            Parameter chainedParameterFirst;
            Parameter chainedParameterSecond;
            Parameter chainedParameterThird;
            if (parameterType == ParameterType.HandleLength)
            {
                if (this.AllParameters.TryGetValue(
                        ParameterType.HandleWidth,
                        out chainedParameterFirst) == true ||
                    this.AllParameters.TryGetValue(
                        ParameterType.RodLength,
                        out chainedParameterSecond) == true)
                {
                    if (this.AllParameters.TryGetValue(
                            ParameterType.HandleWidth,
                            out chainedParameterFirst) == true)
                    {
                        double maxValue = (chainedParameterFirst.Value + 5) * 4;
                        double minValue = (chainedParameterFirst.Value - 5) * 4;
                        if (parameter.Value > maxValue)
                        {
                            exception += "Длина ручки более чем в 4 раза больше её диаметра" +
                                ", уменьшите заданное значение минимум до "
                                + maxValue.ToString() + '\n';
                        }
                        else if (parameter.Value < minValue)
                        {
                            exception += "Длина ручки менее чем в 4 раза больше её диаметра" +
                                ", увеличьте заданное значение минимум до "
                                + minValue.ToString() + '\n';
                        }
                    }

                    if (this.AllParameters.TryGetValue(
                            ParameterType.RodLength,
                            out chainedParameterSecond) == true &&
                        (chainedParameterSecond.Value < parameter.Value) == true)
                    {
                        exception += "Длина ручки больше длины наконечника, " +
                            "уменьшите заданное значение минимум до "
                        + chainedParameterSecond.Value.ToString() + '\n';
                    }
                }
            }

            if (parameterType == ParameterType.HandleWidth)
            {
                if (this.AllParameters.TryGetValue(
                        ParameterType.HandleLength,
                        out chainedParameterFirst) == true ||
                    this.AllParameters.TryGetValue(
                        ParameterType.RodWidth,
                        out chainedParameterSecond) == true)
                {
                    double lowerQuarter = (double)((chainedParameterFirst.Value / 4) - 5);
                    double upperQuarter = (double)((chainedParameterFirst.Value / 4) + 5);
                    if (this.AllParameters.TryGetValue(
                            ParameterType.HandleLength,
                            out chainedParameterFirst) == true &&
                       (parameter.Value < lowerQuarter ||
                       parameter.Value > upperQuarter) == true)
                    {
                        if (parameter.Value < lowerQuarter)
                        {
                            exception += "Диаметр ручки меньше четверти длины ручки - 5 мм" +
                                ", увеличьте заданное значение минимум до "
                                    + Math.Ceiling(lowerQuarter).ToString() + '\n';
                        }
                        else if (parameter.Value > upperQuarter)
                        {
                            exception += "Диаметр ручки больше четверти длины ручки + 5 мм" +
                                ", уменьшите заданное значение минимум до "
                                    + Math.Floor(upperQuarter).ToString() + '\n';
                        }
                    }

                    if (this.AllParameters.TryGetValue(
                            ParameterType.RodWidth,
                            out chainedParameterSecond) == true)
                    {
                        double minValue = chainedParameterSecond.Value * 2;
                        double maxValue = (chainedParameterSecond.Value + 2) * 2;
                        if (parameter.Value < minValue)
                        {
                            exception += "Диаметр ручки не превышает диаметр наконечника " +
                                "в 2 раза, увеличьте заданное значение минимум до "
                               + minValue.ToString() + '\n';
                        }
                        else if (parameter.Value > maxValue)
                        {
                            exception += "Диаметр ручки больше диаметра наконечника " +
                                "более чем в 2 раза, уменьшите заданное значение минимум до "
                               + maxValue.ToString() + '\n';
                        }
                    }
                }
            }
            else if (parameterType == ParameterType.RodLength)
            {
                if (this.AllParameters.TryGetValue(
                        ParameterType.HandleLength,
                        out chainedParameterFirst) == true)
                {
                    if (this.AllParameters.TryGetValue(
                            ParameterType.HandleLength,
                            out chainedParameterFirst) == true &&
                        parameter.Value < chainedParameterFirst.Value)
                    {
                        exception += "Длина наконечника меньше длины ручки, " +
                            "увеличьте заданное значение как минимум до "
                        + chainedParameterFirst.Value.ToString() + '\n';
                    }
                }
            }
            else if (parameterType == ParameterType.RodWidth)
            {
                if (this.AllParameters.TryGetValue(
                        ParameterType.HandleWidth,
                        out chainedParameterThird) == true)
                {
                    double upperHalfOfWidth = (double)chainedParameterThird.Value / 2;
                    double lowerHalfOfWidth = (double)((chainedParameterThird.Value / 2) - 2);
                    if (parameter.Value < lowerHalfOfWidth)
                    {
                        exception += "Диаметр наконечника меньше половины диаметра ручки, " +
                            "увеличьте заданное значение минимум до "
                                    + Math.Ceiling(lowerHalfOfWidth).ToString() + '\n';
                    }
                    else if (parameter.Value > upperHalfOfWidth)
                    {
                        exception += "Диаметр наконечника больше половины диаметра ручки, " +
                            "уменьшите заданное значение минимум до "
                                    + Math.Floor(upperHalfOfWidth).ToString() + '\n';
                    }
                }
            }

            if (exception != string.Empty)
            {
                throw new ArgumentException(exception);
            }
        }
    }
}
