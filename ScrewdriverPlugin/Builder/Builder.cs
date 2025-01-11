using System;
using Kompas;

namespace ScrewdriverPlugin
{
    /// <summary>
    /// Класс для построения модели отвёртки в Компас.
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Одна вторая.
        /// </summary>
        private const double ONESECOND = 0.5;

        /// <summary>
        /// Одна четвёртая.
        /// </summary>
        private const double ONEFOURTH = 0.25;

        /// <summary>
        /// Одна пятая.
        /// </summary>
        private const double ONEFIVED = 0.2;

        /// <summary>
        /// Две третьих.
        /// </summary>
        private const double TWOTHIRD = 0.66;

        /// <summary>
        /// Четыре целых пять десятых поделённое на шесть.
        /// </summary>
        private const double FOURWITHHALFSIXED = 0.75;

        /// <summary>
        /// Одна седьмая.
        /// </summary>
        private const double ONESEVENED = 0.143;

        /// <summary>
        /// Одна сорок пятая.
        /// </summary>
        private const double ONEFOURTYFIVED = 0.0222;

        /// <summary>
        /// Пять шестых.
        /// </summary>
        private const double FIVESIXED = 0.8333;

        /// <summary>
        /// Корень из 2.
        /// </summary>
        private const double SQRT2 = 1.414;

        /// <summary>
        /// Одна десятая.
        /// </summary>
        private const double ONETENTH = 0.1;

        /// <summary>
        /// Экземпляр класс Wrapper.
        /// </summary>
        private Wrapper _wrapper = new Wrapper();

        /// <summary>
        /// Построение отвёртки.
        /// </summary>
        /// <param name="parameters">Параметры отвёртки.</param>
        public void Build(Parameters parameters)
        {
            this._wrapper.OpenCAD();
            this._wrapper.CreateFile();
            this.BuildRod(parameters);
            this.BuildHandle(parameters);
        }

        /// <summary>
        /// Построение наконечника отвёртки.
        /// </summary>
        /// <param name="parameters">Параметры отвёртки.</param>
        private void BuildRod(Parameters parameters)
        {
            this._wrapper.CreateSketch(1);
            Parameter rodLength = parameters.AllParameters[ParameterType.RodLength];
            int y = rodLength.Value;
            Parameter rodWidth = parameters.AllParameters[ParameterType.RodWidth];
            double x1 = -((double)rodWidth.Value);
            double x3 = x1 * ONESECOND;
            double fivedX = x1 * ONEFIVED;
            double newY = y * ONEFIVED / Math.Log10(y * ONEFIVED) /
                (y / (-x3 * 2) * ONESEVENED / Math.Sqrt(y * ONEFOURTYFIVED));
            switch (parameters.ShapeOfRod)
            {
                case RodType.Cruciform:
                {
                    this.BuildCruciform(y, x1, x3, fivedX, newY);
                    break;
                }

                case RodType.Flat:
                {
                    this.BuildFlat(y, x1, x3, newY);
                    break;
                }

                case RodType.Rectangle:
                {
                    this.BuildRectangle(y, x3, fivedX, newY);
                    break;
                }
            }
        }

        /// <summary>
        /// Построение ручки отвёртки.
        /// </summary>
        /// <param name="parameters">Параметры отвёртки.</param>
        private void BuildHandle(Parameters parameters)
        {
            Parameter handleLength = parameters.AllParameters[ParameterType.HandleLength];
            double y1 = -handleLength.Value;
            double y2 = -((double)handleLength.Value) * ONESECOND;
            Parameter handleWidth = parameters.AllParameters[ParameterType.HandleWidth];
            double x2 = -handleWidth.Value;
            double x1 = -handleWidth.Value - (x2 * ONETENTH);
            double quarterX = x2 * ONEFOURTH;
            double halfX = x2 * ONESECOND;
            switch (parameters.ShapeOfHandle)
            {
                case HandleType.Prisme:
                {
                    this.BuildPrisme(halfX, quarterX, y1);
                    break;
                }

                case HandleType.Cylinder:
                {
                    this.BuildCylinder(x1, y1, halfX, y2);
                    break;
                }
            }

            if (parameters.IsHoleExist)
            {
                this.BuildHole(y1, x2);
            }
        }

        /// <summary>
        /// Вспомогательный метод для создания эскиза-создания линии-выдавливания.
        /// </summary>
        /// <param name="points">Точки по которым строятся линии.</param>
        /// <param name="typeExtrusion">Тип выдавливания.</param>
        /// <param name="typeSketch">Плоскость для эскиза.</param>
        /// <param name="extrusionDepth">Глубина выдавливания.</param>
        /// <param name="start">Стартовый индекс массива.</param>
        /// <param name="count">Количество считываемых строк из массива.</param>
        private void Helper(
            double[,] points,
            int[] typeExtrusion,
            int[] typeSketch,
            double[] extrusionDepth,
            int[] start,
            int[] count)
        {
            for (int i = 0; i < typeExtrusion.Length; i++)
            {
                this._wrapper.CreateSketch(typeSketch[i]);
                this._wrapper.CreateLine(points, start[i], count[i]);
                this._wrapper.Extrusion(typeExtrusion[i], extrusionDepth[i]);
            }
        }

        /// <summary>
        /// Метод построения крестового наконечника.
        /// </summary>
        /// <param name="y">Длина наконечника.</param>
        /// <param name="x1">Диаметр наконечника, взятый со знаком минус.</param>
        /// <param name="x3">Одна вторая диаметра наконечника, взятая со знаком минус.</param>
        /// <param name="fivedX">Одна пятая диаметра наконечника, взятая со знаком минус.</param>
        /// <param name="newY">Сложная математическая формула, определяющая на основе параметров размер строимой крестовины.</param>
        private void BuildCruciform(double y, double x1, double x3, double fivedX, double newY)
        {
            double sqrtX = SQRT2 * ONESECOND * x3;
            double[,] pointsArray =
            {
                { 0, 0, x3, 0, 1 },
                { 0, 0, 0, y, 3 },
                { x3, 0, x3, y, 1 },
                { 0, y, x3, y, 1 },
                { 0, y - 1, x3, y - newY, 1 },
                { 0, y - 1, -x3, y - newY, 1 },
                { -x3, y - newY, -x3, y, 1 },
                { x3, y - newY, x3, y, 1 },
                { -x3, y, x3, y, 1 },
                { 0, -y + 1, x3, -y + newY, 1 },
                { 0, -y + 1, -x3, -y + newY, 1 },
                { -x3, -y + newY, -x3, -y, 1 },
                { x3, -y + newY, x3, -y, 1 },
                { -x3, -y, x3, -y, 1 },
                { sqrtX, sqrtX, -sqrtX, -sqrtX, 1, },
                { sqrtX, -sqrtX, -sqrtX, sqrtX, 1, },
                { fivedX, y - 1, x3, y - newY, 1 },
                { -fivedX, y - 1, -x3, y - newY, 1 },
                { -fivedX, y - 1, fivedX, y - 1, 1 },
                { -x3, y - newY, -x3, y, 1 },
                { x3, y - newY, x3, y, 1 },
                { -x3, y, x3, y, 1 },
                { fivedX, -y + 1, x3, -y + newY, 1 },
                { -fivedX, -y + 1, -x3, -y + newY, 1 },
                { -fivedX, -y + 1, fivedX, -y + 1, 1 },
                { -x3, -y + newY, -x3, -y, 1 },
                { x3, -y + newY, x3, -y, 1 },
                { -x3, -y, x3, -y, 1 },
            };
            this._wrapper.CreateLine(pointsArray, 0, 4);
            this._wrapper.Spin();
            int[] typeExtrusion = { 1, 1, 2, 1, 1 };
            int[] typeSketch = { 1, 3, 2, 1, 3 };
            double[] extrusionDepth = { -x1, -x1, y, -x1, -x1 };
            int[] start = { 4, 9, 14, 16, 22 };
            int[] count = { 5, 5, 2, 6, 6 };
            this.Helper(
                pointsArray,
                typeExtrusion,
                typeSketch,
                extrusionDepth,
                start,
                count);
        }

        /// <summary>
        /// Метод построения плоского наконечника.
        /// </summary>
        /// <param name="y">Длина наконечника.</param>
        /// <param name="x1">Диаметр наконечника, взятый со знаком минус.</param>
        /// <param name="x3">Одна вторая диаметра наконечника, взятая со знаком минус.</param>
        /// <param name="newY">Сложная математическая формула, определяющая на основе параметров размер строимой крестовины.</param>
        private void BuildFlat(double y, double x1, double x3, double newY)
        {
            double[,] pointsArray =
            {
                { 0, 0, x3, 0, 1 },
                { 0, 0, 0, y, 3 },
                { x3, 0, x3, y, 1 },
                { 0, y, x3, y, 1 },
                { 0, y - 1, x3, y - newY, 1 },
                { 0, y - 1, -x3, y - newY, 1 },
                { -x3, y - newY, -x3, y, 1 },
                { x3, y - newY, x3, y, 1 },
                { -x3, y, x3, y, 1 },
            };
            this._wrapper.CreateLine(pointsArray, 0, 4);
            this._wrapper.Spin();
            int[] typeExtrusion = { 1 };
            int[] typeSketch = { 1 };
            double[] extrusionDepth = { -x1 };
            int[] start = { 4 };
            int[] count = { 5 };
            this.Helper(
                pointsArray,
                typeExtrusion,
                typeSketch,
                extrusionDepth,
                start,
                count);
        }

        /// <summary>
        /// Метод построения квадратного наконечника.
        /// </summary>
        /// <param name="y">Длина наконечника.</param>
        /// <param name="x3">Одна вторая диаметра наконечника, взятая со знаком минус.</param>
        /// <param name="fivedX">Одна пятая диаметра наконечника, взятая со знаком минус.</param>
        /// <param name="newY">Сложная математическая формула, определяющая на основе параметров размер строимой крестовины.</param>
        private void BuildRectangle(double y, double x3, double fivedX, double newY)
        {
            double[,] pointsArray =
            {
                { 0, 0, x3, 0, 1 },
                { 0, 0, 0, y, 3 },
                { x3, 0, x3, y, 1 },
                { 0, y, x3, y, 1 },
                { x3 - fivedX, y - 1, x3 - fivedX, y - newY, 1 },
                { x3 - fivedX, y - newY, x3, y - newY, 1 },
                { -x3 + fivedX, y - 1, -x3 + fivedX, y - newY, 1 },
                { -x3 + fivedX, y - newY, -x3, y - newY, 1 },
                { -x3 + fivedX, y - 1, x3 - fivedX, y - 1, 1 },
                { -x3, y - newY, -x3, y, 1 },
                { x3, y - newY, x3, y, 1 },
                { -x3, y, x3, y, 1 },
                { x3 - fivedX, -y + 1, x3 - fivedX, -y + newY, 1 },
                { x3 - fivedX, -y + newY, x3, -y + newY, 1 },
                { -x3 + fivedX, -y + 1, -x3 + fivedX, -y + newY, 1 },
                { -x3 + fivedX, -y + newY, -x3, -y + newY, 1 },
                { -x3 + fivedX, -y + 1, x3 - fivedX, -y + 1, 1 },
                { -x3, -y + newY, -x3, -y, 1 },
                { x3, -y + newY, x3, -y, 1 },
                { -x3, -y, x3, -y, 1 },
            };
            this._wrapper.CreateLine(pointsArray, 0, 4);
            this._wrapper.Spin();
            int[] typeExtrusion = { 1, 1 };
            int[] typeSketch = { 1, 3 };
            double[] extrusionDepth = { -x3 * 2, -x3 * 2 };
            int[] start = { 4, 12 };
            int[] count = { 8, 8 };
            this.Helper(
                pointsArray,
                typeExtrusion,
                typeSketch,
                extrusionDepth,
                start,
                count);
        }

        /// <summary>
        /// Метод построения призматической ручки.
        /// </summary>
        /// <param name="halfX">Одна вторая диаметра ручки взятого со знаком минус.</param>
        /// <param name="quarterX">Одна четвёртая диаметра ручки взятого со знаком минус.</param>
        /// <param name="y1">Длина ручки, взятая со знаком минус.</param>
        private void BuildPrisme(double halfX, double quarterX, double y1)
        {
            double[,] pointsArray =
                {
                    { halfX, 0, quarterX, -halfX, 1 },
                    { quarterX, -halfX, -quarterX, -halfX, 1 },
                    { -quarterX, -halfX, -halfX, 0, 1 },
                    { -halfX, 0, -quarterX, halfX, 1 },
                    { -quarterX, halfX, quarterX, halfX, 1 },
                    { quarterX, halfX, halfX, 0, 1 },
                };
            int[] typeExtrusion = { 3 };
            int[] typeSketch = { 2 };
            double[] extrusionDepth = { y1 };
            int[] start = { 0 };
            int[] count = { 6 };
            this.Helper(
                pointsArray,
                typeExtrusion,
                typeSketch,
                extrusionDepth,
                start,
                count);
        }

        /// <summary>
        /// Метод построения цилиндрической ручки.
        /// </summary>
        /// <param name="x1">Диаметр ручки + одна десятая от него, всё со знаком минус.</param>
        /// <param name="y1">Длина ручки, взятая со знаком минус.</param>
        /// <param name="halfX">Одна вторая диаметра ручки взятого со знаком минус.</param>
        /// <param name="y2">Одна вторая длины ручки, взятая со знаком минус.</param>
        private void BuildCylinder(double x1, double y1, double halfX, double y2)
        {
            this._wrapper.CreateSketch(1);
            this._wrapper.CreateArc(x1 / 2, y1, halfX, y2, x1 / 2, 0);
            double[,] pointsArray =
            {
                { 0, y1, 0, 0, 3 },
                { 0, y1, x1 / 2, y1, 1 },
                { 0, 0, x1 / 2, 0, 1 },
            };
            this._wrapper.CreateLine(pointsArray, 0, 3);
            this._wrapper.Spin();
        }

        /// <summary>
        /// Метод построения отверстия в отвёртке.
        /// </summary>
        /// <param name="y1">Длина ручки, взятая со знаком минус.</param>
        /// <param name="x2">Диаметр ручки, взятый со знаком минус.</param>
        private void BuildHole(double y1, double x2)
        {
            this._wrapper.CreateSketch(1);
            this._wrapper.CreateArc(
                0,
                y1 * FIVESIXED,
                x2 * ONEFOURTH,
                y1 * FOURWITHHALFSIXED,
                0,
                y1 * TWOTHIRD);
            this._wrapper.CreateArc(
                0,
                y1 * FIVESIXED,
                -x2 * ONEFOURTH,
                y1 * FOURWITHHALFSIXED,
                0,
                y1 * TWOTHIRD);
            this._wrapper.Extrusion(1, -x2);
        }
    }
}
