using System;
using System.Runtime.InteropServices;
using Kompas6API5;
using Kompas6Constants3D;

namespace Kompas
{
    /// <summary>
    /// Класс для работы с API Компас.
    /// </summary>
    public class Wrapper
    {
        /// <summary>
        /// Поле для хранения приложения Компас.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Поле для хранения выбранной 3d детали.
        /// </summary>
        private Kompas6API5.ksPart _part;

        /// <summary>
        /// Поле для хранения выбранного эскиза.
        /// </summary>
        private Kompas6API5.ksEntity _sketchEntity;

        /// <summary>
        /// Поле для хранения выбранной плоскости.
        /// </summary>
        private Kompas6API5.ksEntity _plane;

        /// <summary>
        /// Создание эскиза в компасе.
        /// </summary>
        /// <param name="perspective">Выбранная плоскость.</param>
        public void CreateSketch(int perspective)
        {
            ksSketchDefinition sketchDef;
            this._sketchEntity = (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_sketch);
            sketchDef = (ksSketchDefinition)this._sketchEntity.GetDefinition();

            if (perspective == 1)
            {
                this._plane = (ksEntity)this._part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            }
            else if (perspective == 2)
            {
               this._plane = (ksEntity)this._part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            }
            else if (perspective == 3)
            {
                this._plane = (ksEntity)this._part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ);
            }

            sketchDef.SetPlane(this._plane);
            this._sketchEntity.Create(); // Создаем эскиз в модели
        }

        /// <summary>
        /// Создание линии в компасе.
        /// </summary>
        /// <param name="pointsArray">Массив точек по которым строятся линии.</param>
        /// <param name="start">Стартовый индекс массива.</param>
        /// <param name="count">Количество считываемых строк из массива.</param>
        public void CreateLine(double[,] pointsArray, int start, int count)
        {
            ksDocument2D document2D;
            ksSketchDefinition sketchDef;
            sketchDef = (ksSketchDefinition)this._sketchEntity.GetDefinition();
            document2D = (ksDocument2D)sketchDef.BeginEdit();
            if (document2D != null)
            {
                for (int i = start; i < start + count; i++)
                {
                    document2D.ksLineSeg(
                        pointsArray[i, 0],
                        pointsArray[i, 1],
                        pointsArray[i, 2],
                        pointsArray[i, 3],
                        (int)pointsArray[i, 4]);
                }

                sketchDef.EndEdit();
            }
        }

        /// <summary>
        /// Создание дуги в компасе.
        /// </summary>
        /// <param name="x1">x координата начальной точки.</param>
        /// <param name="y1">y координата начальной точки.</param>
        /// <param name="x2">x координата промежуточной точки.</param>
        /// <param name="y2">y координата промежуточной точки.</param>
        /// <param name="x3">x координата конечной точки.</param>
        /// <param name="y3">y координата конечной точки.</param>
        public void CreateArc(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            ksDocument2D document2D;
            ksSketchDefinition sketchDef;
            sketchDef = (ksSketchDefinition)this._sketchEntity.GetDefinition();
            document2D = (ksDocument2D)sketchDef.BeginEdit();

            if (document2D != null)
            {
                document2D.ksArcBy3Points(x1, y1, x2, y2, x3, y3, 1);
                sketchDef.EndEdit();
            }
        }

        /// <summary>
        /// Задание вращения в компасе.
        /// </summary>
        public void Spin()
        {
            ksEntity entityRotate = (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate != null)
            {
                ksBossRotatedDefinition rotateDef =
                    (ksBossRotatedDefinition)entityRotate.GetDefinition();
                if (rotateDef != null)
                {
                    rotateDef.directionType = (short)Direction_Type.dtNormal;
                    rotateDef.SetSideParam(false, 360);
                    rotateDef.SetSketch(this._sketchEntity);  // эскиз операции вращения
                    entityRotate.Create();              // создать операцию
                }
            }
        }

        /// <summary>
        /// Выдавливание в компасе.
        /// </summary>
        /// <param name="parameter">Метод выдавливания.</param>
        /// <param name="length">Глубина выдавливания.</param>
        public void Extrusion(int parameter, double length)
        {
            if (parameter == 1)
            {
                ksEntity entityExtrusion =
                    (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtrusion != null)
                {
                    ksEntity entityCutExtrusion =
                        (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                    if (entityCutExtrusion != null)
                    {
                        ksCutExtrusionDefinition cutExtrusionDef =
                            (ksCutExtrusionDefinition)entityCutExtrusion.GetDefinition();
                        if (cutExtrusionDef != null)
                        {
                            cutExtrusionDef.SetSketch(this._sketchEntity);
                            cutExtrusionDef.directionType = (short)Direction_Type.dtBoth;
                            cutExtrusionDef.SetSideParam(
                                true,
                                (short)End_Type.etThroughAll,
                                length,
                                0,
                                false);
                            cutExtrusionDef.SetSideParam(
                                false,
                                (short)End_Type.etThroughAll,
                                length,
                                0,
                                false);
                            cutExtrusionDef.SetThinParam(false, 0, 0, 0);
                        }

                        entityCutExtrusion.Create(); // создадим операцию вырезание выдавливанием
                    }
                }
            }
            else if (parameter == 2)
            {
                ksEntity entityExtrusion =
                    (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtrusion != null)
                {
                    // интерфейс свойств базовой операции выдавливания
                    ksBossExtrusionDefinition extrusionDef =
                        (ksBossExtrusionDefinition)entityExtrusion.GetDefinition();
                    if (extrusionDef != null)
                    {
                        extrusionDef.directionType = (short)Direction_Type.dtNormal;
                        extrusionDef.SetSideParam(
                            true, // прямое направление
                            (short)End_Type.etBlind,    // строго на глубину
                            length,
                            0,
                            false);
                        extrusionDef.SetThinParam(true, (short)Direction_Type.dtBoth, 0.25, 0.25);
                        extrusionDef.SetSketch(this._sketchEntity);   // эскиз операции выдавливания
                        entityExtrusion.Create();                    // создать операцию
                    }
                }
            }
            else if (parameter == 3)
            {
                ksEntity entityExtrusion =
                    (ksEntity)this._part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtrusion != null)
                {
                    // интерфейс свойств базовой операции выдавливания
                    ksBossExtrusionDefinition extrusionDef =
                        (ksBossExtrusionDefinition)entityExtrusion.GetDefinition();
                    if (extrusionDef != null)
                    {
                        ksExtrusionParam extrusionProp =
                            (ksExtrusionParam)extrusionDef.ExtrusionParam();
                        ksThinParam thinProp = (ksThinParam)extrusionDef.ThinParam();
                        if (extrusionProp != null && thinProp != null)
                        {
                            extrusionDef.SetSketch(this._sketchEntity);

                            extrusionProp.direction = (short)Direction_Type.dtNormal;
                            extrusionProp.typeNormal = (short)End_Type.etBlind;
                            extrusionProp.depthNormal = length;

                            thinProp.thin = false;

                            entityExtrusion.Create();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Открытие компаса.
        /// </summary>
        public void OpenCAD()
        {
            try
            {
                // Попытка подключения к существующему процессу Kompas3D
                this._kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                Console.WriteLine("Kompas3D уже запущен.");
            }
            catch
            {
                // Если процесс не найден, создаем новый экземпляр
                Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                this._kompas = (KompasObject)Activator.CreateInstance(kompasType);
                Console.WriteLine("Запущен новый экземпляр Kompas3D.");
            }

            if (this._kompas != null)
            {
                // Делаем окно приложения видимым
                this._kompas.Visible = true;
                this._kompas.ActivateControllerAPI();
                Console.WriteLine("Kompas3D успешно запущен и доступен.");
            }
            else
            {
                Console.WriteLine("Не удалось запустить Kompas3D.");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Создание документа в компасе.
        /// </summary>
        public void CreateFile()
        {
            ksDocument3D document3D;
            document3D = (ksDocument3D)this._kompas.Document3D();
            document3D.Create();
            this._part = (ksPart)document3D.GetPart((short)Part_Type.pTop_Part);
        }
    }
}
