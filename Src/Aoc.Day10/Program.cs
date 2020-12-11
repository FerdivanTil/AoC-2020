using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int> { 28,
                                        33,
                                        18,
                                        42,
                                        31,
                                        14,
                                        46,
                                        20,
                                        48,
                                        47,
                                        24,
                                        23,
                                        49,
                                        45,
                                        19,
                                        38,
                                        39,
                                        11,
                                        1,
                                        32,
                                        25,
                                        35,
                                        8,
                                        17,
                                        7,
                                        9,
                                        4,
                                        2,
                                        34,
                                        10,
                                        3};
            input = new List<int> { 16,
                                    10,
                                    15,
                                    5,
                                    1,
                                    11,
                                    7,
                                    19,
                                    6,
                                    12,
                                    4 };
            input = new List<int> { 1,
                                    2,
                                    3,
                                    4,
                                    5,
                                    6,
                                    7 };
            var text = System.IO.File.ReadAllText(@"input-day-10.txt");
            input = text.Split("\r\n").Select(i => int.Parse(i)).ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static int Test1(List<int> input)
        {
            var dataset = input.OrderBy(i => i).ToList();

            var diffList = new List<long>();
            foreach (var item in Enumerable.Range(0,dataset.Count()-1))
            {
                var diff = dataset[item + 1] - dataset[item];
                diffList.Add(diff);
            }
            Console.WriteLine($"Having {diffList.Count(i => i == 1) + 1} ones and {diffList.Count(i => i == 3) + 1} threes");
            return (diffList.Count(i => i == 1) + 1) * (diffList.Count(i => i == 3) + 1);
        }
        
        private static long Test2(List<int> input)
        {
            var dataset = input.OrderBy(i => i).ToList();
            dataset.Insert(0,0);
            dataset.Add(dataset.Last() + 3);

            var history = new List<Number>() {new Number(0,1)};
            foreach (var item in Enumerable.Range(1, dataset.Count() - 2))
            {
                var total = history.Where(i => i.Key >= (dataset[item] - 3)).Sum(i => i.Value);
                history.Add(new Number(dataset[item], total));
            }
            var i = history.Last().Value;
            Console.WriteLine($"Having {i} combinations");
            return i;
        }
        public class Number
        {
            public int Key { get; set; }
            public long Value { get; set; }
            public Number(int key, long value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
