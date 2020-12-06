using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "abc",
                                            "a\r\nb\r\nc",
                                            "ab\r\nac",
                                            "a\r\na\r\na\r\na",
                                            "b",};

            var text = System.IO.File.ReadAllText(@"input-day-6.txt");
            input = text.Split("\r\n\r\n").ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as sum");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} as sum");

            Console.WriteLine("Done processing.");
        }
        private static int Test1(List<string> input)
        {
            var i = 0;
            foreach(var item in input)
            {
                var combined = item.Replace("\r\n",string.Empty).ToArray().Distinct();
                i += combined.Count();
            }
            return i;
        }
        private static int Test2(List<string> input)
        {
            var i = 0;
            foreach (var item in input)
            {
                var combined = item.Split("\r\n");
                IEnumerable<char> groupResult = combined.First().ToArray();
                foreach(var userInput in combined.Skip(1))
                {
                    groupResult = userInput.ToArray().Intersect(groupResult);
                }
                i += groupResult.Count();
            }
            return i;
        }
    }
}
