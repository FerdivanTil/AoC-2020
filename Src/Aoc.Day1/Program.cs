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

            // Start ordering the two lists
            var lower =  new Queue<int>(input.Where(i => i <= 2020 / 2).OrderBy(i => i).ToArray());
            var upper = new Stack<int>(input.Where(i => i >= 2020 / 2).OrderBy(i => i).ToArray());
            Tuple<int,int> foundNumber = null;
            var first = lower.Dequeue();
            var last = upper.Pop();

            // Lets go
            var i = 0;
            while(true)
            {
                i++;
                var total = first + last;
                if (total == 2020)
                {
                    foundNumber = new Tuple<int, int>(first, last);
                    break;
                }
                if (total > 2020)
                {
                    last = upper.Pop();
                    continue;
                }
                first = lower.Dequeue();
            }
            
            stopwatch.Stop();
            Console.WriteLine($"It took: {stopwatch.ElapsedMilliseconds} and {i} iterations");
            Console.WriteLine($"{foundNumber.Item1}-{foundNumber.Item2}");
            Console.WriteLine($"Result = {foundNumber.Item1*foundNumber.Item2}");
            Console.ReadKey();
        }
    }
}
