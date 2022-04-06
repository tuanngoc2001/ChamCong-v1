using ChamCong.Common.Utils;
using System;

namespace test01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Utils.GetConfig("ConnectionStrings:MyDb"));
        }
    }
}
