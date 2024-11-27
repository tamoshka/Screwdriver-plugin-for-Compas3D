using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ScrewdriverPlugin.UnitTests
{
    /// <summary>
    /// Класс Unit тестов класса <see cref="Parameters"/>.
    /// </summary>
    [TestFixture]
    public class ParametersTests
    {
        /// <summary>
        /// Тестовые параметры
        /// </summary>
        private Parameters _parameters = new Parameters();
        
        /// <summary>
        /// Позитивный тест геттера AllParameters.
        /// </summary>
        [Test(Description = "Позитивный тест геттера AllParameters.")]
        public void TestProjectGetParameters()
        {
            Dictionary<ParameterType, Parameter> expected = new Dictionary<ParameterType,
                Parameter>()
            {
            };
            _parameters.AllParameters = new Dictionary<ParameterType, Parameter>()
            {
            };
            var actual = _parameters;
            Assert.AreEqual(expected, actual.AllParameters);
        }

        /// <summary>
        /// Позитивный тест сеттера AllParameters.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера AllParameters.")]
        public void TestProjectSetParameters()
        {
            _parameters.AllParameters = new Dictionary<ParameterType, Parameter>()
            {
            };
            Parameters expected = new Parameters();
            expected.AllParameters = new Dictionary<ParameterType, Parameter>()
            {
               
            };
            var actual = _parameters;
            Assert.AreEqual(expected.AllParameters, actual.AllParameters);
        }

        /// <summary>
        /// Позитивный тест геттера ShapeOfHandle.
        /// </summary>
        [Test(Description = "Позитивный тест геттера ShapeOfHandle.")]
        public void TestProjectGetHandle()
        {
            var expected = HandleType.Cylinder;
            _parameters.ShapeOfHandle = HandleType.Cylinder;
            var actual = _parameters;
            Assert.AreEqual(expected, actual.ShapeOfHandle);
        }

        /// <summary>
        /// Позитивный тест сеттера ShapeOfHandle.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера ShapeOfHandle.")]
        public void TestProjectSetHandle()
        {
            Parameters expected = new Parameters();
            _parameters.ShapeOfHandle = HandleType.Cylinder;
            expected.ShapeOfHandle= HandleType.Cylinder;
            var actual = _parameters;
            Assert.AreEqual(expected.ShapeOfHandle, actual.ShapeOfHandle);
        }

        /// <summary>
        /// Позитивный тест геттера ShapeOfRod.
        /// </summary>
        [Test(Description = "Позитивный тест геттера ShapeOfRod.")]
        public void TestProjectGetRod()
        {
            var expected = RodType.Cruciform;
            _parameters.ShapeOfRod = RodType.Cruciform;
            var actual = _parameters;
            Assert.AreEqual(expected, actual.ShapeOfRod);
        }

        /// <summary>
        /// Позитивный тест сеттера ShapeOfRod.
        /// </summary>
        [Test(Description = "Позитивный тест сеттера ShapeOfRod.")]
        public void TestProjectSetRod()
        {
            Parameters expected = new Parameters();
            _parameters.ShapeOfRod = RodType.Cruciform;
            expected.ShapeOfRod= RodType.Cruciform;
            var actual = _parameters;
            Assert.AreEqual(expected.ShapeOfRod, actual.ShapeOfRod);
        }

        /// <summary>
        /// Позитивный тест метода SetParameter.
        /// </summary>
        [Test(Description = "Позитивный тест метода SetParameter.")]
        public void TestProjectSetParameter()
        {
            Parameter parameter = new Parameter();
            parameter.MaxValue = 20;
            parameter.MinValue = 10;
            parameter.Value = 15;
            Parameters expected = new Parameters();
            _parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            expected.AllParameters = new Dictionary<ParameterType, Parameter>();
            _parameters.SetParameter(ParameterType.HandleWidth, parameter);
            expected.SetParameter(ParameterType.HandleWidth, parameter);
            var actual = _parameters;
            Assert.AreEqual(expected.AllParameters, actual.AllParameters);
        }

        /// <summary>
        /// TestCase методов проверки сеттера свойства Value.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="wrongArgument">Неверный аргумент.</param>
        /// <param name="message">Текст ошибки.</param>
        [TestCase(ParameterType.HandleLength, 121, "Должно возникать исключение, если " +
            "HandleLength более чем в 4 раза больше HandleWidth",
            TestName = "Длина ручки более чем в 4 раза больше её диаметра, уменьшите " +
            "заданное значение минимум до 120")]
        [TestCase(ParameterType.HandleLength, 79, "Должно возникать исключение, если " +
            "HandleLength менее чем в 4 раза больше HandleWidth",
            TestName = "Длина ручки менее чем в 4 раза больше её диаметра, увеличьте " +
            "заданное значение минимум до 80")]
        [TestCase(ParameterType.HandleLength, 101, "Должно возникать исключение, если " +
            "HandleLength больше чем RodLength",
            TestName = "Длина ручки больше длины наконечника, уменьшите заданное " +
            "значение минимум до 100")]
        [TestCase(ParameterType.HandleWidth, 27, "Должно возникать исключение, если HandleWidth " +
            "более чем в 2 раза больше RodWidth",
            TestName = "Диаметр ручки больше диаметра наконечника более чем в 2 раза, уменьшите " +
            "заданное значение минимум до 26")]
        [TestCase(ParameterType.HandleWidth, 21, "Должно возникать исключение, если HandleWidth " +
            "менее чем в 2 раза больше RodWidth",
            TestName = "Диаметр ручки больше диаметра наконечника менее чем в 2 раза, увеличьте " +
            "заданное значение минимум до 22")]
        [TestCase(ParameterType.HandleWidth, 19, "Должно возникать исключение, если HandleWidth " +
            "более чем в 4 раза меньше HandleLength",
            TestName = "Диаметр ручки меньше четверти длины ручки - 5 мм, увеличьте заданное " +
            "значение минимум до 20")]
        [TestCase(ParameterType.HandleWidth, 31, "Должно возникать исключение, если HandleWidth " +
            "менее чем в 4 раза меньше HandleLength",
            TestName = "Диаметр ручки больше четверти длины ручки + 5 мм, уменьшите заданное " +
            "значение минимум до 30")]
        [TestCase(ParameterType.RodLength, 99, "Должно возникать исключение, если RodLength " +
            "меньше чем HandleLength",
            TestName = "Длина наконечника меньше длины ручки, увеличьте заданное значение " +
            "как минимум до 100")]
        [TestCase(ParameterType.RodWidth, 13, "Должно возникать исключение, если RodWidth" +
            " менее чем в 2 раза меньше HandleWidth",
            TestName = "Диаметр наконечника меньше половины диаметра ручки, " +
            "увеличьте заданное значение минимум до 12")]
        [TestCase(ParameterType.RodWidth, 10, "Должно возникать исключение, " +
            "если RodWidth более чем в 2 раза меньше HandleWidth",
            TestName = "Диаметр наконечника больше половины диаметра ручки, " +
            "уменьшите заданное значение минимум до 11")]
        public void TestSetArgumentException(ParameterType parameterType, 
            int wrongArgument, string message)
        {
            Parameter handleLength = new Parameter();
            handleLength.MaxValue = 150;
            handleLength.MinValue = 45;
            handleLength.Value = 100;
            Parameter handleWidth = new Parameter();
            handleWidth.MaxValue = 42;
            handleWidth.MinValue = 7;
            handleWidth.Value = 25;
            Parameter rodLength = new Parameter();
            rodLength.MaxValue = 500;
            rodLength.MinValue = 45;
            rodLength.Value = 100;
            Parameter rodWidth = new Parameter();
            rodWidth.MaxValue = 21;
            rodWidth.MinValue = 3;
            rodWidth.Value = 11;
            _parameters.AllParameters=new Dictionary<ParameterType, Parameter>();
            _parameters.SetParameter(ParameterType.HandleLength, handleLength);
            _parameters.SetParameter(ParameterType.HandleWidth, handleWidth);
            _parameters.SetParameter(ParameterType.RodLength, rodLength);
            _parameters.SetParameter(ParameterType.RodWidth, rodWidth);
            Parameter newParameter = new Parameter();
            newParameter.MaxValue = 500;
            newParameter.MinValue = 3;
            newParameter.Value = wrongArgument;
            Assert.Throws<ArgumentException>(
            () => { _parameters.SetParameter(parameterType, newParameter); },
            message);
        }
    }
}
