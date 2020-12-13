using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "F10",
                                            "N3",
                                            "F7",
                                            "R90",
                                            "F11"};

            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split("\r\n").ToList();

            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static int Test1(List<string> input)
        {
            var navigator = new Navigator1();
            foreach(var item in input)
            {
                navigator.Goto(item);
            }
            return navigator.GetDistance();
        }
        private static int Test2(List<string> input)
        {
            var navigator = new Navigator2();
            foreach (var item in input)
            {
                navigator.Goto(item);
            }
            return navigator.GetDistance();
        }
    }
}
