using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6Constants;
using Kompas6API2D5COM;
using Kompas6API3D5COM;
using Kompas6Constants3D;
using Kompas6API5;
using KompasAPI7;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;

namespace Kompas
{

    public class Wrapper 
    {
        private KompasObject _kompas;

        private Kompas6API5.ksDocument3D _document3D;

        private Kompas6API5.ksPart _part;

        private Kompas6API5.ksEntity _sketchEntity;

        private Kompas6API5.ksEntity _plane;

        private Kompas6API5.ksSketchDefinition _sketchDef;

        private Kompas6API5.ksDocument2D _document2D;

        private Kompas6API5.ksEntity _sketchForExtrusion;

        private Kompas6API5.ksSketchDefinition _sketchDefForExtrusion;



        public void CreateSketch(int perspective)
        {
            _sketchEntity = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDef = (ksSketchDefinition)_sketchEntity.GetDefinition();

            if (perspective == 1)
            {
                _plane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            }
            else if (perspective == 2) 
            {
                _plane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            }
            else if(perspective == 3)
            {
                _plane = (ksEntity)_part.GetDefaultEntity((short)Obj3dType.o3d_planeYOZ);
            }
            _sketchDef.SetPlane(_plane);
            _sketchEntity.Create(); // Создаем эскиз в модели
        }

        public void CreateLine(double x1, double y1, double x2, double y2, int style)
        {
            _document2D = (ksDocument2D)_sketchDef.BeginEdit();

            if (_document2D != null)
            {
                _document2D.ksLineSeg(x1, y1, x2, y2, style);
                
                _sketchDef.EndEdit();
            }
        }

        public void CreateArc(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            _document2D = (ksDocument2D)_sketchDef.BeginEdit();

            if (_document2D != null)
            {
                _document2D.ksArcBy3Points(x1, y1, x2, y2, x3, y3, 1);
                _sketchDef.EndEdit();
            }
        }

        public void Spin()
        {
            ksEntity entityRotate = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate != null)
            {
                ksBossRotatedDefinition rotateDef = (ksBossRotatedDefinition)entityRotate.GetDefinition(); // интерфейс базовой операции вращения
                if (rotateDef != null)
                {
                    rotateDef.directionType = (short)Direction_Type.dtNormal;   // тонкая стенка в два направления
                    rotateDef.SetSideParam(false, 360);
                    rotateDef.SetSketch(_sketchEntity);  // эскиз операции вращения
                    entityRotate.Create();              // создать операцию
                }
            }
        }


        public void Extrusion(int parameter, double length)
        {
            if (parameter == 1)
            {
                ksEntity entityExtr = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtr != null)
                {
                    ksEntity entityCutExtr = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                    if (entityCutExtr != null)
                    {
                        ksCutExtrusionDefinition cutExtrDef = (ksCutExtrusionDefinition)entityCutExtr.GetDefinition();
                        if (cutExtrDef != null)
                        {
                            cutExtrDef.SetSketch(_sketchEntity);    // установим эскиз операции
                            cutExtrDef.directionType = (short)Direction_Type.dtBoth; //прямое направление
                            cutExtrDef.SetSideParam(true, (short)End_Type.etBlind, length, 0, false);
                            cutExtrDef.SetThinParam(false, 0, 0, 0);
                        }

                        entityCutExtr.Create(); // создадим операцию вырезание выдавливанием
                    }
                }
            }
            else if (parameter == 2)
            {
                ksEntity entityExtr = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtr != null)
                {
                    // интерфейс свойств базовой операции выдавливания
                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                    if (extrusionDef != null)
                    {
                        extrusionDef.directionType = (short)Direction_Type.dtNormal;         // направление выдавливания
                        extrusionDef.SetSideParam(true, // прямое направление
                            (short)End_Type.etBlind,    // строго на глубину
                            length, 0, false);
                        extrusionDef.SetThinParam(true, (short)Direction_Type.dtBoth, 0.25, 0.25); // тонкая стенка в два направления
                        extrusionDef.SetSketch(_sketchEntity);   // эскиз операции выдавливания
                        entityExtr.Create();                    // создать операцию
                    }
                }
            }
            else if (parameter == 3)
            {
                ksEntity entityExtr = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
                if (entityExtr != null)
                {
                    // интерфейс свойств базовой операции выдавливания
                    ksBossExtrusionDefinition extrusionDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition(); // интерфейс базовой операции выдавливания
                    if (extrusionDef != null)
                    {
                        ksExtrusionParam extrProp = (ksExtrusionParam)extrusionDef.ExtrusionParam(); // интерфейс структуры параметров выдавливания
                        ksThinParam thinProp = (ksThinParam)extrusionDef.ThinParam();      // интерфейс структуры параметров тонкой стенки
                        if (extrProp != null && thinProp != null)
                        {
                            extrusionDef.SetSketch(_sketchEntity); // эскиз операции выдавливания

                            extrProp.direction = (short)Direction_Type.dtNormal;      // направление выдавливания (прямое)
                            extrProp.typeNormal = (short)End_Type.etBlind;      // тип выдавливания (строго на глубину)
                            extrProp.depthNormal = length;         // глубина выдавливания

                            thinProp.thin = false;              // без тонкой стенки

                            entityExtr.Create();                // создадим операцию
                        }
                    }
                }
            }
        }

        public void OpenCAD()
        {
            try
            {
                // Попытка подключения к существующему процессу Kompas3D
                _kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                Console.WriteLine("Kompas3D уже запущен.");
            }
            catch
            {
                // Если процесс не найден, создаем новый экземпляр
                Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompas = (KompasObject)Activator.CreateInstance(kompasType);
                Console.WriteLine("Запущен новый экземпляр Kompas3D.");
            }

            if (_kompas != null)
            {
                // Делаем окно приложения видимым
                _kompas.Visible = true;
                _kompas.ActivateControllerAPI();
                Console.WriteLine("Kompas3D успешно запущен и доступен.");
            }
            else
            {
                Console.WriteLine("Не удалось запустить Kompas3D.");
            }

            Console.ReadLine();
        }

        public void CreateFile()
        {
            _document3D = (ksDocument3D)_kompas.Document3D();
            _document3D.Create();
            _part = (ksPart)_document3D.GetPart((short)Part_Type.pTop_Part);
        }

    }
}
