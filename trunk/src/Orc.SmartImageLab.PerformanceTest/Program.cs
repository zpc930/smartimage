using System;
using System.Collections.Generic;
using System.Text;
using Orc.Util;

namespace Orc.SmartImageLab.PerformanceTest
{
    public class Program
    {
        public static void Main()
        {
            UnmanagedImageTest.Test();

            Console.WriteLine("Test Finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
