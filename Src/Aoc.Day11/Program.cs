using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "L.LL.LL.LL",
"LLLLLLL.LL",
"L.L.L..L..",
"LLLL.LL.LL",
"L.LL.LL.LL",
"L.LLLLL.LL",
"..L.L.....",
"LLLLLLLLLL",
"L.LLLLLL.L",
"L.LLLLL.LL"};

            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split("\r\n").ToList();
            var input2 = new List<List<char>>();
            foreach (var row in input)
            {
                input2.Add(row.ToList());
            }
            //var i = Test1(input2);

            //Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input2);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static int Test1(List<List<char>> input)
        {
            var processor = new SeatLayout(input);
            while(processor.Start(4))
            {
            }
            return processor.GetOccupiedCount();
        }

        private static int Test2(List<List<char>> input)
        {
            var processor = new SeatLayout(input);
            while (processor.Start(5))
            {
                var output = processor.GetState();
                Console.WriteLine(output);
                Console.WriteLine();
            }
            return processor.GetOccupiedCount();
        }
    }
}
