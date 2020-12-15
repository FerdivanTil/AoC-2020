using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Aoc.Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "939",
                                            "7,13,x,x,59,x,31,19"};
            input = new List<string> { "0",
                                            "17,x,13,19"};
            //input = new List<string> { "0",
            //                                "67,x,7,59,61"};



            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split("\r\n").ToList();

            var timestamp = int.Parse(input[0]);
            var busnumbers = input[1].Split(",").Where(i => i != "x").Select(i => int.Parse(i)).ToList();
            var i = Test1(timestamp, busnumbers);

            Console.WriteLine($"Test1: Found {i} as result");

            var busnumbers2 = input[1].Split(",").ToList();

            var j = Test2(busnumbers2);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static int Test1(int timestamp, List<int> input)
        {
            var bestBus = 0;
            var bestOffset = int.MaxValue;
            foreach(var item in input)
            {
                var busOffset = item - timestamp % item;
                if(busOffset < bestOffset)
                {
                    bestBus = item;
                    bestOffset = busOffset;
                }

            }
            return bestOffset * bestBus;
        }

        private static long Test2(List<string> input)
        {
            var sequenceToFind = new List<int>();
            var j = 0;
            foreach (var item in input.Skip(1))
            {
                j++;
                if (item != "x")
                {
                    sequenceToFind.Add(j);
                }
            }
            //var sequenceFound = false;
            var dataset = input.Where(i => i != "x").Select(i => int.Parse(i)).ToList();
            long i = dataset[0];
            long timestamp = 0;
            foreach (var item in Enumerable.Range(0, sequenceToFind.Count()))
            {
                var offset = sequenceToFind[item];
                var bus = dataset[item +1];
                while ((timestamp + offset) % bus != 0)
                {
                    timestamp += i;
                }

                i *= bus;
            }
            return timestamp;
        }
    }
}
