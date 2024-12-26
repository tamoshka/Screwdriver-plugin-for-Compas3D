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
        private const double DIF = 5;

        /// <summary>
        /// Определяет разницу в диаметре.
        /// </summary>
        private const double DIAMETERDIF = 2;

        /// <summary>
        /// Определяет половину размера.
        /// </summary>
        private const double HALF = 2;

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
        /// Поле хранящее в себе информацию о наличии отверстия для возможности "повесить" отвёртку.
        /// </summary>
        private bool _isHoleExist;

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
        /// <param name="parameter">Параметр.</param>
        public void SetParameter(Parameter parameter)
        {
            try
            {
                this.DefineMinMax(parameter);
                parameter.Validator();
                this.AllParameters.Remove(parameter.TypeOfParameter);
                this.AllParameters.Add(parameter.TypeOfParameter, parameter);
                this.ValidateParameters(parameter);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
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
                    parameter.MinValue = 45;
                    parameter.MaxValue = 150;
                    break;
                case ParameterType.HandleWidth:
                    parameter.MinValue = 7;
                    parameter.MaxValue = 42;
                    break;
                case ParameterType.RodLength:
                    parameter.MinValue = 45;
                    parameter.MaxValue = 500;
                    break;
                case ParameterType.RodWidth:
                    parameter.MinValue = 3;
                    parameter.MaxValue = 21;
                    break;
            }
        }

        /// <summary>
        /// Валидация зависимых параметров.
        /// </summary>
        /// <exception cref="ArgumentException">Текст ошибки.</exception>
        private void ValidateParameters(Parameter parameter)
        {
            //TODO: rename
            string message = string.Empty;
            //TODO: redo
            switch (parameter.TypeOfParameter)
            {
                //TODO: RSDN
                case ParameterType.HandleLength:
                {
                    if (this.AllParameters.TryGetValue(
                        ParameterType.HandleWidth,
                        out Parameter chainedParameterFirst) ||
                    this.AllParameters.TryGetValue(
                        ParameterType.RodLength,
                        out Parameter chainedParameterSecond))
                    {
                        if (this.AllParameters.TryGetValue(
                                ParameterType.HandleWidth,
                                out chainedParameterFirst))
                        {
                            //TODO: const
                            double maxValue = (chainedParameterFirst.Value + DIF) * FOURPLE;
                            double minValue = (chainedParameterFirst.Value - DIF) * FOURPLE;
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
                            (chainedParameterSecond.Value < parameter.Value) == true)
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
                    if (this.AllParameters.TryGetValue(
                        ParameterType.HandleLength,
                        out Parameter chainedParameterFirst) == true ||
                    this.AllParameters.TryGetValue(
                        ParameterType.RodWidth,
                        out Parameter chainedParameterSecond) == true)
                    {
                        if (this.AllParameters.TryGetValue(
                                ParameterType.HandleLength,
                                out chainedParameterFirst) == true)
                        {
                            //TODO: const
                            double lowerQuarter = (double)((chainedParameterFirst.Value / FOURPLE) - DIF);
                            double upperQuarter = (double)((chainedParameterFirst.Value / FOURPLE) + DIF);
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

                        if (this.AllParameters.TryGetValue(
                                ParameterType.RodWidth,
                                out chainedParameterSecond) == true)
                        {
                            //TODO: const
                            double minValue = chainedParameterSecond.Value * HALF;
                            double maxValue = (chainedParameterSecond.Value + DIAMETERDIF) * HALF;
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
                        out Parameter chainedParameterFirst) == true)
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
                        out Parameter chainedParameterThird) == true)
                    {
                        double upperHalfOfWidth = (double)chainedParameterThird.Value;
                        upperHalfOfWidth = upperHalfOfWidth / HALF;
                        double lowerHalfOfWidth = (double)chainedParameterThird.Value;
                        lowerHalfOfWidth = (lowerHalfOfWidth / HALF) - DIAMETERDIF;
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
