using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScrewdriverPlugin

{
    public class Parameters
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
            if (parameterType == ParameterType.HandleLength)
            {
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true ||
                    AllParameters.TryGetValue(ParameterType.RodLength, out chainedParameterSecond) == true)
                {
                    if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterFirst) == true &&
                         (chainedParameterFirst.Value < parameter.Value / 4 - 5 ||
                         chainedParameterFirst.Value > parameter.Value / 4 + 5)==true)
                    {
                        if (chainedParameterFirst.Value < parameter.Value / 4 - 5)
                        {
                            exception += "Длина ручки более чем в 4 раза больше её диаметра, уменьшите заданное значение минимум до "
                                + ((chainedParameterFirst.Value+5)*4).ToString()+'\n';
                        }
                        else
                        {
                            exception += "Длина ручки менее чем в 4 раза больше её диаметра, увеличьте заданное значение минимум до "
                                + ((chainedParameterFirst.Value -5) * 4).ToString()+'\n';
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
                    if (AllParameters.TryGetValue(ParameterType.HandleLength, out chainedParameterFirst) == true &&
                    (parameter.Value < chainedParameterFirst.Value / 4 - 5 ||
                    parameter.Value > chainedParameterFirst.Value / 4 + 5) == true)
                    {
                        if (parameter.Value < chainedParameterFirst.Value / 4 - 5)
                        {
                            exception += "Диаметр ручки меньше четверти длины ручки - 5 мм, увеличьте заданное значение минимум до "
                                    + (chainedParameterFirst.Value / 4 - 5).ToString()+'\n';
                        }
                        else
                        {
                            exception += "Диаметр ручки больше четверти длины ручки + 5 мм, уменьшите заданное значение минимум до "
                                    + (chainedParameterFirst.Value / 4 + 5).ToString()+'\n'; 
                        }
                    }
                    if (AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true)
                    {
                        if (AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true &&
                            (parameter.Value/2<chainedParameterSecond.Value)==true)
                        {
                            exception += "Диаметр ручки не превышает диаметр наконечника в 2 раза, увеличьте заданное значение минимум до "
                               + (chainedParameterSecond.Value *2).ToString()+'\n';
                        }
                        else if(AllParameters.TryGetValue(ParameterType.RodWidth, out chainedParameterSecond) == true &&
                            (parameter.Value / 2 - 2 > chainedParameterSecond.Value) == true)
                        {
                            exception += "Диаметр ручки больше диаметра наконечника более чем в 2 раза, уменьшите заданное значение минимум до "
                               + ((chainedParameterSecond.Value+2)*2).ToString()+'\n';
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
                if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterThird) == true &&
                    (parameter.Value<chainedParameterThird.Value/2-2) == true)
                {
                    exception += "Диаметр наконечника меньше половины диаметра ручки, увеличьте заданное значение минимум до "
                                + (chainedParameterThird.Value/2-2).ToString()+'\n';
                }
                else if (AllParameters.TryGetValue(ParameterType.HandleWidth, out chainedParameterThird) == true &&
                    (parameter.Value > chainedParameterThird.Value / 2)==true)
                {
                    exception += "Диаметр наконечника больше половины диаметра ручки, уменьшите заданное значение минимум до "
                                + (chainedParameterThird.Value / 2).ToString()+'\n';
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
