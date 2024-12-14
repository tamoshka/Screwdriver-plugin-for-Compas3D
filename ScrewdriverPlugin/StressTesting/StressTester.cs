using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NickStrupat;
using ScrewdriverPlugin;

namespace StressTesting
{
    /// <summary>
    /// Класс нагрузочного тестирования.
    /// </summary>
    public class StressTester
    {
        /// <summary>
        /// Метод для нагрузочного тестирования.
        /// </summary>
        public void StressTesting()
        {
            var builder = new Builder();
            var stopWatch = new Stopwatch();
            var parameters = new Parameters();
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
            parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            parameters.SetParameter(ParameterType.HandleLength, handleLength);
            parameters.SetParameter(ParameterType.HandleWidth, handleWidth);
            parameters.SetParameter(ParameterType.RodLength, rodLength);
            parameters.SetParameter(ParameterType.RodWidth, rodWidth);
            parameters.ShapeOfHandle = HandleType.Cylinder;
            parameters.ShapeOfRod = RodType.Cruciform;
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var count = 0;
            var streamWriter = new StreamWriter("log.txt");
            const double gigabyteInByte = 0.000000000931322574615478515625;
            while (true)
            {
                stopWatch.Start();
                builder.Build(parameters);
                stopWatch.Stop();
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory
                                  - computerInfo.AvailablePhysicalMemory)
                                  * gigabyteInByte;
                streamWriter.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
                stopWatch.Reset();
            }

            streamWriter.Close();
            streamWriter.Dispose();
            Console.WriteLine($"End {new ComputerInfo().TotalPhysicalMemory}");
        }
    }
}
