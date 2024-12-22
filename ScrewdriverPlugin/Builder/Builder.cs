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
            Parameter rodLength;
            parameters.AllParameters.TryGetValue(ParameterType.RodLength, out rodLength);
            int y = rodLength.Value;
            Parameter rodWidth;
            parameters.AllParameters.TryGetValue(ParameterType.RodWidth, out rodWidth);
            double x1 = -rodWidth.Value;
            x1 = x1 / 2;
            double fivedX = x1 / 5;
            double newY = y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45));
            switch (parameters.ShapeOfRod)
            {
                case RodType.Cruciform:
                    double sqrtX = Math.Sqrt(2) / 2 * x1;
                    double[,] pointsArrayCruciform =
                    {
                        { 0, 0, x1, 0, 1 },
                        { 0, 0, 0, y, 3 },
                        { x1, 0, x1, y, 1 },
                        { 0, y, x1, y, 1 },
                        { 0, y - 1, x1, y - newY, 1 },
                        { 0, y - 1, -x1, y - newY, 1 },
                        { -x1, y - newY, -x1, y, 1 },
                        { x1, y - newY, x1, y, 1 },
                        { -x1, y, x1, y, 1 },
                        { 0, -y + 1, x1, -y + newY, 1 },
                        { 0, -y + 1, -x1, -y + newY, 1 },
                        { -x1, -y + newY, -x1, -y, 1 },
                        { x1, -y + newY, x1, -y, 1 },
                        { -x1, -y, x1, -y, 1 },
                        {
                            sqrtX, sqrtX,
                            -sqrtX, -sqrtX, 1,
                        },
                        {
                            sqrtX, -sqrtX,
                            -sqrtX, sqrtX, 1,
                        },
                        { fivedX, y - 1, x1, y - newY, 1 },
                        { -fivedX, y - 1, -x1, y - newY, 1 },
                        { -fivedX, y - 1, fivedX, y - 1, 1 },
                        { -x1, y - newY, -x1, y, 1 },
                        { x1, y - newY, x1, y, 1 },
                        { -x1, y, x1, y, 1 },
                        { fivedX, -y + 1, x1, -y + newY, 1 },
                        { -fivedX, -y + 1, -x1, -y + newY, 1 },
                        { -fivedX, -y + 1, fivedX, -y + 1, 1 },
                        { -x1, -y + newY, -x1, -y, 1 },
                        { x1, -y + newY, x1, -y, 1 },
                        { -x1, -y, x1, -y, 1 },
                    };
                    this._wrapper.CreateLine(pointsArrayCruciform, 0, 4);
                    this._wrapper.Spin();
                    int[] typeExtrusionCruciform = { 1, 1, 2, 1, 1 };
                    int[] typeSketchCruciform = { 1, 3, 2, 1, 3 };
                    double[] extrusionDepthCruciform = { -x1 * 2, -x1 * 2, y, -x1 * 2, -x1 * 2 };
                    int[] startCruciform = { 4, 9, 14, 16, 22 };
                    int[] countCruciform = { 5, 5, 2, 6, 6 };
                    this.Helper(pointsArrayCruciform, typeExtrusionCruciform, typeSketchCruciform, extrusionDepthCruciform, startCruciform, countCruciform);
                    break;
                case RodType.Flat:
                    double[,] pointsArrayFlat =
                    {
                        { 0, 0, x1, 0, 1 },
                        { 0, 0, 0, y, 3 },
                        { x1, 0, x1, y, 1 },
                        { 0, y, x1, y, 1 },
                        { 0, y - 1, x1, y - newY, 1 },
                        { 0, y - 1, -x1, y - newY, 1 },
                        { -x1, y - newY, -x1, y, 1 },
                        { x1, y - newY, x1, y, 1 },
                        { -x1, y, x1, y, 1 },
                    };
                    this._wrapper.CreateLine(pointsArrayFlat, 0, 4);
                    this._wrapper.Spin();
                    int[] typeExtrusionFlat = { 1 };
                    int[] typeSketchFlat = { 1 };
                    double[] extrusionDepthFlat = { -x1 * 2 };
                    int[] startFlat = { 4 };
                    int[] countFlat = { 5 };
                    this.Helper(pointsArrayFlat, typeExtrusionFlat, typeSketchFlat, extrusionDepthFlat, startFlat, countFlat);
                    break;
                case RodType.Rectangle:
                    double[,] pointsArrayRectangle =
                    {
                        { 0, 0, x1, 0, 1 },
                        { 0, 0, 0, y, 3 },
                        { x1, 0, x1, y, 1 },
                        { 0, y, x1, y, 1 },
                        { x1 - fivedX, y - 1, x1 - fivedX, y - newY, 1 },
                        { x1 - fivedX, y - newY, x1, y - newY, 1 },
                        { -x1 + fivedX, y - 1, -x1 + fivedX, y - newY, 1 },
                        { -x1 + fivedX, y - newY, -x1, y - newY, 1 },
                        { -x1 + fivedX, y - 1, x1 - fivedX, y - 1, 1 },
                        { -x1, y - newY, -x1, y, 1 },
                        { x1, y - newY, x1, y, 1 },
                        { -x1, y, x1, y, 1 },
                        { x1 - fivedX, -y + 1, x1 - fivedX, -y + newY, 1 },
                        { x1 - fivedX, -y + newY, x1, -y + newY, 1 },
                        { -x1 + fivedX, -y + 1, -x1 + fivedX, -y + newY, 1 },
                        { -x1 + fivedX, -y + newY, -x1, -y + newY, 1 },
                        { -x1 + fivedX, -y + 1, x1 - fivedX, -y + 1, 1 },
                        { -x1, -y + newY, -x1, -y, 1 },
                        { x1, -y + newY, x1, -y, 1 },
                        { -x1, -y, x1, -y, 1 },
                    };
                    this._wrapper.CreateLine(pointsArrayRectangle, 0, 4);
                    this._wrapper.Spin();
                    int[] typeExtrusionRectangle = { 1, 1 };
                    int[] typeSketchRectangle = { 1, 3 };
                    double[] extrusionDepthRectangle = { -x1 * 2, -x1 * 2 };
                    int[] startRectangle = { 4, 12 };
                    int[] countRectangle = { 8, 8 };
                    this.Helper(pointsArrayRectangle, typeExtrusionRectangle, typeSketchRectangle, extrusionDepthRectangle, startRectangle, countRectangle);
                    break;
            }
        }

        /// <summary>
        /// Построение ручки отвёртки.
        /// </summary>
        /// <param name="parameters">Параметры отвёртки.</param>
        private void BuildHandle(Parameters parameters)
        {
            Parameter handleLength;
            parameters.AllParameters.TryGetValue(ParameterType.HandleLength, out handleLength);
            double y1 = -handleLength.Value;
            double y2 = -handleLength.Value;
            y2 = y2 / 2;
            double y3 = 0;
            Parameter handleWidth;
            parameters.AllParameters.TryGetValue(ParameterType.HandleWidth, out handleWidth);
            double x2 = -handleWidth.Value;
            double x1 = -handleWidth.Value - (x2 / 10);
            double x3 = -handleWidth.Value - (x2 / 10);
            double quarterX = x2 / 4;
            double halfX = x2 / 2;
            switch (parameters.ShapeOfHandle)
            {
                case HandleType.Prisme:
                    double[,] pointsArrayPrisme =
                    {
                        { halfX, 0, quarterX, -halfX, 1 },
                        { quarterX, -halfX, -quarterX, -halfX, 1 },
                        { -quarterX, -halfX, -halfX, 0, 1 },
                        { -halfX, 0, -quarterX, halfX, 1 },
                        { -quarterX, halfX, quarterX, halfX, 1 },
                        { quarterX, halfX, halfX, 0, 1 },
                    };
                    int[] typeExtrusionPrisme = { 3 };
                    int[] typeSketchPrisme = { 2 };
                    double[] extrusionDepthPrisme = { y1 };
                    int[] startPrisme = { 0 };
                    int[] countPrisme = { 6 };
                    this.Helper(pointsArrayPrisme, typeExtrusionPrisme, typeSketchPrisme, extrusionDepthPrisme, startPrisme, countPrisme);
                    break;
                case HandleType.Cylinder:
                    this._wrapper.CreateSketch(1);
                    this._wrapper.CreateArc(x1 / 2, y1, halfX, y2, x3 / 2, y3);
                    double[,] pointsArrayCylinder =
                    {
                        { 0, y1, 0, y3, 3 },
                        { 0, y1, x1 / 2, y1, 1 },
                        { 0, y3, x3 / 2, y3, 1 },
                    };
                    this._wrapper.CreateLine(pointsArrayCylinder, 0, 3);
                    this._wrapper.Spin();
                    break;
            }

            if (parameters.IsHoleExist == true)
            {
                this._wrapper.CreateSketch(1);
                this._wrapper.CreateArc(0, y1 * 5 / 6, x2 / 4, y1 * 4.5 / 6, 0, y1 * 2 / 3);
                this._wrapper.CreateArc(0, y1 * 5 / 6, -x2 / 4, y1 * 4.5 / 6, 0, y1 * 2 / 3);
                this._wrapper.Extrusion(1, -x2);
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
    }
}
