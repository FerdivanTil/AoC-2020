using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                                            "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                                            "bright white bags contain 1 shiny gold bag.",
                                            "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                                            "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                                            "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                                            "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                                            "faded blue bags contain no other bags.",
                                            "dotted black bags contain no other bags."};

            var text = System.IO.File.ReadAllText(@"input-day-7.txt");
            input = text.Split("\r\n").ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as sum");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} as sum");

            Console.WriteLine("Done processing.");
        }
        private static List<Bag> Parse(List<string> input)
        {
            var bagsResult = new List<Bag>();
            foreach(var item in input)
            {
                var first = item.Split(" bags contain ");
                var name = first[0];
                var bag = new Bag(first[0]);
                bagsResult.Add(bag);
                if (first[1] == "no other bags.")
                    continue;
                var bags = first[1].Split(", ");
                var regex = new Regex(@"^(?<amount>[0-9]{1})\s(?<name>[a-z]+\s[a-z]+)\sbags?\.?$");

                foreach (var bagReference in bags)
                {
                    var bagresult = regex.Match(bagReference);
                    var bagname = bagresult.Groups["name"].Value;
                    
                    var bagItem = new Bag(bagname);
                    var bagreference = new BagReference(int.Parse(bagresult.Groups["amount"].Value), bagItem);
                    bag.BagReference.Add(bagreference);
                }
            }
            return bagsResult;
        }

        private static int Test1(List<string> input)
        {
            var myBag = "shiny gold";
            var i = 0;
            var parsedInput = Parse(input);
            var list = new Queue<Bag>(parsedInput.Where(i => i.BagReference.Any(x => x.Bag.Name == myBag)));
            parsedInput.RemoveAll(i => i.BagReference.Any(x => x.Bag.Name == myBag));
            //i = list.Count();
            while(list.Any())
            {
                i++;
                var item = list.Dequeue();
                
                var references = parsedInput.Where(i => i.BagReference.Any(x => x.Bag.Name == item.Name)).ToList();
                foreach(var reference in references)
                {
                    list.Enqueue(reference);
                    parsedInput.Remove(reference);
                }
            }
            return i;
        }

        private static int Test2(List<string> input)
        {
            var myBag = "shiny gold";
            var i = 0;
            var parsedInput = Parse(input);
            var list = new Queue<Tuple<int,BagReference>>(parsedInput.First(i => i.Name == myBag).BagReference.Select(i => new Tuple<int, BagReference>(i.Amount,i)));
            while (list.Any())
            {
                var item = list.Dequeue();
                i += item.Item1;
                var references = parsedInput.First(i => i.Name == item.Item2.Bag.Name).BagReference;
                foreach (var reference in references)
                {
                    list.Enqueue(new Tuple<int, BagReference>(item.Item1 * reference.Amount, reference));
                }
            }
            return i;
        }
    }
    public class Bag
    {
        public Bag(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<BagReference> BagReference { get; set; } = new List<BagReference>();
    }
    public class BagReference
    {
        public BagReference(int amount, Bag bag)
        {
            Amount = amount;
            Bag = bag;
            //Parent.Add(parent);
        }
        public int Amount { get; set; }
        public Bag Bag { get; set; }
    }
}
