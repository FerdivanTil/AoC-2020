using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Aoc.Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "0,3,6".Split(',').Select(i => int.Parse(i)).ToList();

            //input = "1,3,2".Split(',').Select(i => int.Parse(i)).ToList();
            //input = "2,1,3".Split(',').Select(i => int.Parse(i)).ToList();
            input = "2,1,10,11,0,6".Split(',').Select(i => int.Parse(i)).ToList();


            //var text = System.IO.File.ReadAllText(@"input.txt");
            //input = text.Split("\r\n").ToList();

            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }
        private static long Test1(List<int> input)
        {
            return Play(input, 2020);
        }

        private static long Play(List<int> input, int amount)
        {
            var spoken = input.Select((item, index) => new Number(item, index +1)).ToDictionary(i => i.Value, i=> i);
            var last = input.Last();
            foreach(var item in Enumerable.Range(spoken.Count() +1, amount))
            {
                var current = spoken[last];
                last = current.NewNumber;
                spoken.AddOrSet(last, item);

                if(item % 1000000 == 0)
                {
                    Console.WriteLine($"Currently @ {item}");
                }
            }
            var lastSpoken =  spoken.First(i => i.Value.Last == amount).Value.Value;
            return lastSpoken;
        }

        private static long Test2(List<int> input)
        {
            return Play(input, 30000000);
        }
    }

    [DebuggerDisplay("Value = {Value} Index = {string.Join(',',Index)}")]
    public class Number
    {
        public int Value { get; set; }
        public int Last { get; set; }
        public int SecondLast { get; set; }
        public bool IsFirstTime => SecondLast == 0;

        public int NewNumber => IsFirstTime? 0 : Last - SecondLast;

        public void AddIndex(int number)
        {
            SecondLast = Last;
            Last = number;
        }
        public Number(int value, int index)
        {
            Value = value;
            Last = index;
        }
    }
}
