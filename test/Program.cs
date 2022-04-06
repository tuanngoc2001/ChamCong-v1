using ChamCong.Common.Utils;
using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = Utils.GetConfig("ConnectionStrings:MyDb");
            Console.WriteLine(test);
        }
    }
}
