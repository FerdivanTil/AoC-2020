using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "class: 1-3 or 5-7\r\nrow: 6-11 or 33-44\r\nseat: 13-40 or 45-50\r\n\r\nyour ticket:\r\n7,1,14\r\n\r\nnearby tickets:\r\n7,3,47\r\n40,4,50\r\n55,2,20\r\n38,6,12".Split("\r\n\r\n").ToList();

            input = "class: 0-1 or 4-19\r\nrow: 0-5 or 8-19\r\nseat: 0-13 or 16-19\r\n\r\nyour ticket: \r\n11,12,13\r\n\r\nnearby tickets: \r\n3,9,18\r\n15,1,5\r\n5,14,9".Split("\r\n\r\n").ToList();
            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split("\r\n\r\n").ToList();

            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static Input Parse(List<string> input)
        {
            var result = new Input();
            var regex = new Regex(@"^(?<name>[a-z\s]+):\s(?<min1>\d+)-(?<max1>\d+) or (?<min2>\d+)-(?<max2>\d+)$");
            var i = 0;
            foreach(var item in input[0].Split(Environment.NewLine))
            {
                var parsed = regex.Match(item);
                result.Rules.Add(new Rule(parsed.Groups["name"].Value, int.Parse(parsed.Groups["min1"].Value), int.Parse(parsed.Groups["max1"].Value), int.Parse(parsed.Groups["min2"].Value), int.Parse(parsed.Groups["max2"].Value), i));
                i++;
            }
            result.MyValues = input[1].Split(Environment.NewLine).Skip(1).First().Split(',').Select(i => int.Parse(i)).ToList();
            result.NearBy = input[2].Split(Environment.NewLine).Skip(1).Select(i => i.Split(",").Select(i => int.Parse(i)).ToList()).ToList();
            return result;
        }

        private static long Test1(List<string> input)
        {
            var parsed = Parse(input);
            var result = new List<int>();
            foreach(var item in parsed.NearBy)
            {
                foreach(var number in item)
                {
                    if(parsed.IsValid(number))
                    {
                        result.Add(number);
                    }
                }
            }
            return result.Sum();
        }

        private static long Test2(List<string> input)
        {
            var parsed = Parse(input);
            var result = new List<int>();
            // Remove all invalid
            var validList = parsed.NearBy.Where(i => !i.Any(x => parsed.Rules.All(y => !y.IsValid(x)))).ToList();

            // Just to make sure, add my own value to the validation list
            validList.Add(parsed.MyValues);

            var validRules = new List<List<Rule>>();
            foreach(var colum in Enumerable.Range(0, parsed.MyValues.Count))
            {
                var columvalues = validList.Select(i => i[colum]).ToList();
                var rules = parsed.Rules.Where(i => columvalues.All(c => i.IsValid(c))).ToList();
                var rule = columvalues.Where(i => !parsed.Rules[0].IsValid(i));
                validRules.Add(rules);
            }

            // Find unique values
            var possible = validRules.Select(i => i.Select(i => i.Name).ToList()).ToList();
            while (possible.Any(i => i.Count > 1))
            {
                var foundItems = possible.Where(i => i.Count == 1);
                var foundValues = foundItems.Select(i => i.First()).ToList();
                foreach (var item in possible.Except(foundItems))
                {
                    item.RemoveFromAll(foundValues);
                }
            }

            // Get all the values from my values
            var output = possible.SelectMany(i => i).Select((name,index) => (name,index)).Where(i => i.name.StartsWith("departure")).Select(i => parsed.MyValues[i.index]);

            // Multiply the values
            return output.Select(i => i).Aggregate(1L, (x, y) => x * y);

        }
    }
    public static class extention
    {
        public static void RemoveFromAll(this List<string> self,List<string> names)
        {
            foreach(var item in names)
            {
                if(self.Contains(item))
                {
                    self.Remove(item);
                }
            }
        }
    }
    public class Input
    {
        public List<Rule> Rules { get; set; } = new List<Rule>();
        public List<int> MyValues { get; set; }
        public List<List<int>> NearBy { get; set; }
        public bool IsValid(int input)
        {
            return Rules.All(i => !i.IsValid(input));
        }
    }

    public class Rule
    {
        public Rule(string name, int min1, int max1, int min2, int max2, int index)
        {
            Name = name;
            Min1 = min1;
            Max1 = max1;
            Min2 = min2;
            Max2 = max2;
            Index = index;
        }
        public int Index { get; set; }
        public string Name { get; set; }
        public int Min1 { get; set; }
        public int Max1 { get; set; }
        public int Min2 { get; set; }
        public int Max2 { get; set; }
        public bool IsValid(int input)
        {
            return input >= Min1 && input <= Max1 || input >= Min2 && input <= Max2;
        }
    }
}
