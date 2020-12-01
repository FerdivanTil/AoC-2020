using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc.Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test data
            var input = new List<int> { 1721,
                                        979,
                                        366,
                                        299,
                                        675,
                                        1456};
            // Real live data
            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split(Environment.NewLine).Select(i => int.Parse(i)).ToList();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var i = Test1(input);

            stopwatch.Stop();
            Console.WriteLine($"Test 1: It took: {stopwatch.ElapsedMilliseconds}ms and {i} iterations");

            stopwatch.Restart();
            i = Test2(input);

            stopwatch.Stop();
            Console.WriteLine($"Test 2: It took: {stopwatch.ElapsedMilliseconds}ms and {i} iterations");
        }

        private static int Test1(List<int> input)
        {
            var dataset = input.OrderBy(i => i).ToList();
            Tuple<int, int> foundNumber = null;

            // Lets go
            var i = 0;
            var last = dataset.Count() - 1;
            var first = 0;
            while (true)
            {
                i++;
                var total = dataset[first] + dataset[last];
                if (total == 2020)
                {
                    foundNumber = new Tuple<int, int>(dataset[first], dataset[last]);
                    break;
                }
                if (total > 2020)
                {
                    last--;
                    continue;
                }
                first++;
            }

            Console.WriteLine($"{foundNumber.Item1}-{foundNumber.Item2}");
            Console.WriteLine($"Result = {foundNumber.Item1 * foundNumber.Item2}");
            return i;
        }

        private static int Test2(List<int> input)
        {
            Tuple<int, int, int> foundNumber = null;
            var dataset = input.OrderBy(i => i).ToList();
            var i = 0;
            var round = 0;
            foreach(var item in dataset)
            {
                var last = dataset.Count() -1;
                var first = round + 1;
                while(true)
                {
                    i++;
                    var total = item + dataset[first] + dataset[last];
                    if (total == 2020)
                    {
                        foundNumber = new Tuple<int, int, int>(item, dataset[first], dataset[last]);
                        break;
                    }
                    if(total > 2020)
                    {
                        last--;
                    }
                    if (total < 2020)
                    {
                        first++;
                    }
                    if(last == first)
                    {
                        break;
                    }
                }
                if(foundNumber != null)
                {
                    break;
                }
                round++;
            }

            Console.WriteLine($"{foundNumber.Item1}-{foundNumber.Item2}-{foundNumber.Item3}");
            Console.WriteLine($"Result = {foundNumber.Item1 * foundNumber.Item2 * foundNumber.Item3}");

            return i;
        }
    }
}
