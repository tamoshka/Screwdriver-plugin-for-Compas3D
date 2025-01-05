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
            parameters.AllParameters = new Dictionary<ParameterType, Parameter>();
            parameters.SetParameter(ParameterType.HandleLength, 100);
            parameters.SetParameter(ParameterType.HandleWidth, 25);
            parameters.SetParameter(ParameterType.RodLength, 100);
            parameters.SetParameter(ParameterType.RodWidth, 11);
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
