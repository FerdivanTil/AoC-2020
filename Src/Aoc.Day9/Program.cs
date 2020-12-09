using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day9
{
    class Program
    {
        private static int preamble = 5;
        static void Main(string[] args)
        {
            var input = new List<long> { 35,
                                            20,
                                            15,
                                            25,
                                            47,
                                            40,
                                            62,
                                            55,
                                            65,
                                            95,
                                            102,
                                            117,
                                            150,
                                            182,
                                            127,
                                            219,
                                            299,
                                            277,
                                            309,
                                            576};

            var text = System.IO.File.ReadAllText(@"input-day-9.txt");
            input = text.Split("\r\n").Select(i => long.Parse(i)).ToList();
            preamble = 25;
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            i = Test2(input, i);

            Console.WriteLine($"Test2: Found {i} as result");

            Console.WriteLine("Done processing.");
        }
        private static long Test1(List<long> input)
        {
            foreach(var i in Enumerable.Range(0,input.Count- preamble))
            {
                var output = FindSum(input.Skip(i).Take(preamble), input.Skip(i + preamble).First());
                if(output > 0)
                {
                    return output;
                }
            }
            return -1;
            
        }
        private static long Test2(List<long> input, long numberToFind)
        {
            foreach (var item in Enumerable.Range(0, input.Count))
            {
                var i = 1;
                var subset = new List<long> { input[item] };
                while (i != -1)
                {
                    subset.Add(input[item + i]);
                    if (subset.Sum() == numberToFind)
                    {
                        Console.WriteLine($"Item found with: {string.Join(", ", subset.ToArray())}");
                        var orderedList = subset.OrderBy(i => i);
                        return orderedList.First() + orderedList.Last();
                    }
                    if (subset.Sum() > numberToFind)
                    {
                        break;
                    }
                    i++;
                }
            }
            return -1;
        }
        private static long FindSum(IEnumerable<long> input, long value)
        {
            var foundNumber = 0;
            var dataset = input.OrderBy(i => i).ToList();
            var last = dataset.Count() - 1;
            var first = 0;
            while (true)
            {
                if(first == last)
                { 
                    Console.WriteLine($"Sum NOT found for value {value}");
                    return value;
                }
                var total = dataset[first] + dataset[last];
                if (total == value)
                {
                    Console.WriteLine($"Sum found for value {value}: {dataset[first]} - {dataset[last]}");
                    break;
                }
                if (total > value)
                {
                    last--;
                    continue;
                }
                first++;
            }
            return foundNumber;
        }
    }
}
