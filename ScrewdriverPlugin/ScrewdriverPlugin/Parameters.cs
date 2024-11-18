using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewdriverPlugin

{
    internal class Parameters
    {
        private Dictionary<ParameterType, Parameter> _parameter;

        private Dictionary<ParameterType, Parameter> _parameters;

        private HandleType _handleType;

        private RodType _rodType;

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

        private void ValidateParameters()
        {
            string exception = "";
            ParameterType parameterType=_parameter.ElementAt(0).Key;
            Parameter parameter = _parameter.ElementAt(0).Value;
            Parameter chainedParameterFirst;
            Parameter chainedParameterSecond;
            Parameter chainedParameterThird;
            /*if (parameterType == ParameterType.HandleLength)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true ||
                    AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true ||
                    AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterThird) == true)
                {
                    if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true &&
                         (chainedParameterFirst.Value < parameter.Value / 4 - 5 ||
                         chainedParameterFirst.Value > parameter.Value / 4 + 5)==true)
                    {
                        if (chainedParameterFirst.Value < parameter.Value / 4 - 5)
                        {
                            exception += "Диаметр ручки меньше четверти длины ручки - 5 мм, увеличьте заданное значение минимум до "
                                + (parameter.Value / 4 - 5).ToString();
                        }
                        else
                        {
                            exception += "Диаметр ручки больше четверти длины ручки + 5 мм, уменьшите заданное значение минимум до "
                                + (parameter.Value / 4 + 5).ToString(); ;
                        }
                    }
                    if (AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true &&
                         (chainedParameterSecond.Value < parameter.Value) == true)
                    {
                        exception += "Длина наконечника меньше длины ручки, увеличьте заданное значение как минимум до "
                        + (parameter.Value).ToString();
                    }
                    if (AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterThird) == true &&
                        AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true &&
                        (chainedParameterThird.Value < (parameter.Value + chainedParameterSecond.Value) / 5 - 2 ||
                        chainedParameterThird.Value > (parameter.Value + chainedParameterSecond.Value) / 5 + 2) == true)
                    {
                        if (chainedParameterThird.Value < (parameter.Value + chainedParameterSecond.Value) / 5 - 2)
                        {
                            exception += "Диаметр наконечника меньше пятой части от общей длины отвёртки - 2 мм, увеличьте заданное значение минимум до "
                                + ((parameter.Value + chainedParameterSecond.Value) / 5 - 2).ToString();
                        }
                        else
                        {
                            exception += "Диаметр наконечника большей пятой части от общей длины отвёртки + 2 мм, уменьшите заданное значение минимум до "
                                + ((parameter.Value + chainedParameterSecond.Value) / 5 + 2).ToString();
                        }
                    }
                }
            }*/
            if (parameterType == ParameterType.HandleWidth)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                    (parameter.Value < chainedParameterFirst.Value / 4 - 5 ||
                    parameter.Value > chainedParameterFirst.Value / 4 + 5) == true)
                {
                    if (parameter.Value < chainedParameterFirst.Value / 4 - 5)
                    {
                        exception += "Диаметр ручки меньше четверти длины ручки - 5 мм, увеличьте заданное значение минимум до "
                                + (chainedParameterFirst.Value / 4 - 5).ToString();
                    }
                    else
                    {
                        exception += "Диаметр ручки больше четверти длины ручки + 5 мм, уменьшите заданное значение минимум до "
                                + (chainedParameterFirst.Value / 4 + 5).ToString(); ;
                    }
                }
            }
            else if (parameterType == ParameterType.RodLength)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true)
                {
                    /*if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                    AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true &&
                    (chainedParameterSecond.Value < (parameter.Value + chainedParameterFirst.Value) / 5 - 2 ||
                        chainedParameterSecond.Value > (parameter.Value + chainedParameterFirst.Value) / 5 + 2) == true)                  
                    {
                        if (chainedParameterSecond.Value < (parameter.Value + chainedParameterFirst.Value) / 5 - 2)
                        {
                            exception += "Диаметр наконечника меньше пятой части от общей длины отвёртки - 2 мм, увеличьте заданное значение минимум до "
                                + ((parameter.Value + chainedParameterFirst.Value) / 5 - 2).ToString();
                        }
                        else
                        {
                            exception += "Диаметр наконечника большей пятой части от общей длины отвёртки + 2 мм, уменьшите заданное значение минимум до "
                                + ((parameter.Value + chainedParameterFirst.Value) / 5 + 2).ToString();
                        }
                    }*/
                    if(AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                        parameter.Value<chainedParameterFirst.Value)
                    {
                        exception += "Длина наконечника меньше длины ручки, увеличьте заданное значение как минимум до "
                        + (chainedParameterFirst.Value).ToString();
                    }

                }
            }
            else if (parameterType == ParameterType.RodWidth)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterThird) == true &&
                    (parameter.Value>chainedParameterThird.Value/2) == true)
                {
                    exception += "Диаметр наконечника большей диаметра ручки, уменьшите заданное значение минимум до "
                                + (chainedParameterThird.Value/2).ToString();
                }
            }
            if (exception != "")
            {
                throw new ArgumentException(exception);
            }
        }

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
