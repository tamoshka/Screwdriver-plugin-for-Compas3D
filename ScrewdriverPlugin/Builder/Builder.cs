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
            if (parameters.ShapeOfRod == RodType.Cruciform)
            {
                double[,] pointsArray =
                {
                    { 0, 0, x1, 0, 1 },
                    { 0, 0, 0, y, 3 },
                    { x1, 0, x1, y, 1 },
                    { 0, y, x1, y, 1 },
                    { 0, y - 1, x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { 0, y - 1, -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), -x1, y, 1 },
                    { x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), x1, y, 1 },
                    { -x1, y, x1, y, 1 },
                    { 0, -y + 1, x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { 0, -y + 1, -x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), -x1, -y, 1 },
                    { x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), x1, -y, 1 },
                    { -x1, -y, x1, -y, 1 },
                    {
                        Math.Sqrt(2) / 2 * x1, Math.Sqrt(2) / 2 * x1,
                        -Math.Sqrt(2) / 2 * x1, -Math.Sqrt(2) / 2 * x1, 1,
                    },
                    {
                        Math.Sqrt(2) / 2 * x1, -Math.Sqrt(2) / 2 * x1,
                        -Math.Sqrt(2) / 2 * x1, Math.Sqrt(2) / 2 * x1, 1,
                    },
                    { x1 / 5, y - 1, x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1 / 5, y - 1, -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1 / 5, y - 1, x1 / 5, y - 1, 1 },
                    { -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), -x1, y, 1 },
                    { x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), x1, y, 1 },
                    { -x1, y, x1, y, 1 },
                    { x1 / 5, -y + 1, x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1 / 5, -y + 1, -x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1 / 5, -y + 1, x1 / 5, -y + 1, 1 },
                    { -x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), -x1, -y, 1 },
                    { x1, -y + (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), x1, -y, 1 },
                    { -x1, -y, x1, -y, 1 },
                };
                this._wrapper.CreateLine(pointsArray, 0, 4);
                this._wrapper.Spin();
                int[] typeExtrusion = { 1, 1, 2, 1, 1 };
                int[] typeSketch = { 1, 3, 2, 1, 3 };
                double[] extrusionDepth = { -x1 * 2, -x1 * 2, y, -x1 * 2, -x1 * 2 };
                int[] start = { 4, 9, 14, 16, 22 };
                int[] count = { 5, 5, 2, 6, 6 };
                this.Helper(pointsArray, typeExtrusion, typeSketch, extrusionDepth, start, count);
            }
            else
            {
                double[,] pointsArray =
                {
                    { 0, 0, x1, 0, 1 },
                    { 0, 0, 0, y, 3 },
                    { x1, 0, x1, y, 1 },
                    { 0, y, x1, y, 1 },
                    { 0, y - 1, x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { 0, y - 1, -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), 1 },
                    { -x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), -x1, y, 1 },
                    { x1, y - (y / 5 / Math.Log10(y / 5) / (y / (-x1 * 2) / 7 / Math.Sqrt(y / 45))), x1, y, 1 },
                    { -x1, y, x1, y, 1 },
                };
                this._wrapper.CreateLine(pointsArray, 0, 4);
                this._wrapper.Spin();
                int[] typeExtrusion = { 1 };
                int[] typeSketch = { 1 };
                double[] extrusionDepth = { -x1 * 2 };
                int[] start = { 4 };
                int[] count = { 5 };
                this.Helper(pointsArray, typeExtrusion, typeSketch, extrusionDepth, start, count);
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
            if (parameters.ShapeOfHandle == HandleType.Prisme)
            {
                double[,] pointsArray =
                {
                    { x2 / 2, 0, x2 / 4, -x2 / 2, 1 },
                    { x2 / 4, -x2 / 2, -x2 / 4, -x2 / 2, 1 },
                    { -x2 / 4, -x2 / 2, -x2 / 2, 0, 1 },
                    { -x2 / 2, 0, -x2 / 4, x2 / 2, 1 },
                    { -x2 / 4, x2 / 2, x2 / 4, x2 / 2, 1 },
                    { x2 / 4, x2 / 2, x2 / 2, 0, 1 },
                };
                int[] typeExtrusion = { 3 };
                int[] typeSketch = { 2 };
                double[] extrusionDepth = { y1 };
                int[] start = { 0 };
                int[] count = { 6 };
                this.Helper(pointsArray, typeExtrusion, typeSketch, extrusionDepth, start, count);
            }
            else
            {
                this._wrapper.CreateSketch(1);
                this._wrapper.CreateArc(x1 / 2, y1, x2 / 2, y2, x3 / 2, y3);
                double[,] pointsArray =
                {
                    { 0, y1, 0, y3, 3 },
                    { 0, y1, x1 / 2, y1, 1 },
                    { 0, y3, x3 / 2, y3, 1 },
                };
                this._wrapper.CreateLine(pointsArray, 0, 3);
                this._wrapper.Spin();
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
