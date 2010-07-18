using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImageLab.PerformanceTest
{
    public class TestAdd
    {
        public static Double Foo()
        {
            Double result = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                result += 0.1;
            }
            return result;
        }

        public static void Test()
        {
            CodeTimer.Time("TestAdd", 1, () => { TestAdd.Foo(); });
            CodeTimer.Time("TestAdd", 1, () => { TestAdd.Foo(); });
            CodeTimer.Time("TestAdd", 1, () => { TestAdd.Foo(); });
            CodeTimer.Time("TestAdd", 1, () => { TestAdd.Foo(); });
        }
    }
}
