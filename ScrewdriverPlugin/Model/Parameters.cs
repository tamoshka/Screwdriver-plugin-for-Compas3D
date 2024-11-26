using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScrewdriverPlugin

{
    /// <summary>
    /// Класс параметры
    /// </summary>
    public class Parameters
    {
        private Dictionary<ParameterType, Parameter> _parameter;

        private Dictionary<ParameterType, Parameter> _parameters;

        private HandleType _handleType;

        private RodType _rodType;

        /// <summary>
        /// Свойство для _parameters содержащее в себе словарь всех параметров
        /// </summary>
        public Dictionary<ParameterType, Parameter> AllParameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        /// <summary>
        /// Метод для добавления нового параметра в словарь
        /// </summary>
        /// <param name="parameterType">Тип добавляемого параметра</param>
        /// <param name="parameter">Параметр</param>
        public void SetParameter(ParameterType parameterType, Parameter parameter)
        {
            _parameter = new Dictionary<ParameterType, Parameter>()
            {
                {parameterType, parameter }
            };
            AllParameters.Remove(parameterType);
            AllParameters.Add(parameterType, parameter);
            ValidateParameters();
        }

        /// <summary>
        /// Валидация зависимых параметров
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void ValidateParameters()
        {
            string exception = "";
            ParameterType parameterType=_parameter.ElementAt(0).Key;
            Parameter parameter = _parameter.ElementAt(0).Value;
            Parameter chainedParameterFirst;
            Parameter chainedParameterSecond;
            Parameter chainedParameterThird;
            if (parameterType == ParameterType.HandleLength)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true ||
                    AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true)
                {
                    double maxValue = (chainedParameterFirst.Value + 5) * 4;
                    double minValue = (chainedParameterFirst.Value - 5) * 4;
                    if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true &&
                         (parameter.Value < minValue ||
                         parameter.Value > maxValue)==true)
                    {
                        if (parameter.Value > maxValue)
                        {
                            exception += "Длина ручки более чем в 4 раза больше её диаметра, уменьшите заданное значение минимум до "
                                + maxValue.ToString()+'\n';
                        }
                        else if (parameter.Value < minValue)
                        {
                            exception += "Длина ручки менее чем в 4 раза больше её диаметра, увеличьте заданное значение минимум до "
                                + minValue.ToString()+'\n';
                        }
                    }
                    if (AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true &&
                         (chainedParameterSecond.Value < parameter.Value) == true)
                    {
                        exception += "Длина ручки больше длины наконечника, уменьшите заданное значение минимум до "
                        + (chainedParameterSecond.Value).ToString()+'\n';
                    }
                }
            }
            if (parameterType == ParameterType.HandleWidth)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true ||
                    AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true)
                {
                    double lowerQuarter = (double)chainedParameterFirst.Value/4 - 5;
                    double upperQuarter = (double)chainedParameterFirst.Value/4 + 5;
                    if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                    (parameter.Value < lowerQuarter ||
                    parameter.Value > upperQuarter) == true)
                    {
                        if (parameter.Value < lowerQuarter)
                        {
                            exception += "Диаметр ручки меньше четверти длины ручки - 5 мм, увеличьте заданное значение минимум до "
                                    + lowerQuarter.ToString()+'\n';
                        }
                        else if (parameter.Value > upperQuarter)
                        {
                            exception += "Диаметр ручки больше четверти длины ручки + 5 мм, уменьшите заданное значение минимум до "
                                    + upperQuarter.ToString()+'\n'; 
                        }
                    }
                    if (AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true)
                    {
                        double minValue = chainedParameterSecond.Value * 2;
                        double maxValue = (chainedParameterSecond.Value + 2)*2;
                        if (parameter.Value < minValue)
                        {
                            exception += "Диаметр ручки не превышает диаметр наконечника в 2 раза, увеличьте заданное значение минимум до "
                               + minValue.ToString()+'\n';
                        }
                        else if(parameter.Value > maxValue)
                        {
                            exception += "Диаметр ручки больше диаметра наконечника более чем в 2 раза, уменьшите заданное значение минимум до "
                               + maxValue.ToString()+'\n';
                        }
                    }
                }
            }
            else if (parameterType == ParameterType.RodLength)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true)
                {
                    if(AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                        parameter.Value<chainedParameterFirst.Value)
                    {
                        exception += "Длина наконечника меньше длины ручки, увеличьте заданное значение как минимум до "
                        + (chainedParameterFirst.Value).ToString()+'\n';
                    }

                }
            }
            else if (parameterType == ParameterType.RodWidth)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterThird) == true)
                {
                    double upperHalfOfWidth = (double)chainedParameterThird.Value / 2;
                    double lowerHalfOfWidth = (double)chainedParameterThird.Value / 2 - 2;
                    if (parameter.Value < lowerHalfOfWidth)
                    {
                        exception += "Диаметр наконечника меньше половины диаметра ручки, увеличьте заданное значение минимум до "
                                    + lowerHalfOfWidth.ToString() + '\n';
                    }
                    else if (parameter.Value > upperHalfOfWidth)
                    {
                        exception += "Диаметр наконечника больше половины диаметра ручки, уменьшите заданное значение минимум до "
                                    + upperHalfOfWidth.ToString() + '\n';
                    }
                }
            }
            if (exception != "")
            {
                throw new ArgumentException(exception);
            }
        }

        /// <summary>
        /// Свойство для поля _handleType (тип ручки)
        /// </summary>
        public HandleType ShapeOfHandle
        {
            get
            {
                return _handleType;
            }
            set
            {
                _handleType = value;
            }
        }

        /// <summary>
        /// Свойство для поля _rodType (тип наконечника)
        /// </summary>
        public RodType ShapeOfRod
        {
            get
            {
                return _rodType;
            }
            set
            {
                _rodType = value;
            }
        }
    }
}
