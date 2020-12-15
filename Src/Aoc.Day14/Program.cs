using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
                                            "mem[8] = 11",
                                            "mem[7] = 101",
                                            "mem[8] = 0"};
            input = new List<string> { "mask = 000000000000000000000000000000X1001X",
                                        "mem[42] = 100",
                                        "mask = 00000000000000000000000000000000X0XX",
                                        "mem[26] = 1"};

            var text = System.IO.File.ReadAllText(@"input.txt");
            input = text.Split("\r\n").ToList();

            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as result");

            var j = Test2(input);

            Console.WriteLine($"Test2: Found {j} as result");

            Console.WriteLine("Done processing.");
        }

        private static long Test1(List<string> input)
        {
            long zero = 0;
            long ones = 0;

            var regex = new Regex(@"^mem\[(?<mem>\d+)\]\s=\s(?<value>\d+)$");
            var output = new Dictionary<int, long>();
            foreach(var item in input)
            {
                var mask = string.Empty;
                if (item.StartsWith("mask"))
                {
                    zero = 0;
                    ones = 0;
                    mask = item.Split("= ")[1];
                    Console.WriteLine($"Changing mask {mask}");
                    foreach (var maskItem in mask.Reverse().Select((value, i) => (value, i)))
                    {
                        if (maskItem.value == '1')
                        {
                            ones |= (long) Math.Pow(2, maskItem.i);
                        }
                        if (maskItem.value == '0')
                        {
                            zero |= (long) Math.Pow(2, maskItem.i);
                        }
                    }
                    Console.WriteLine($"To ones:      {Convert.ToString(ones, 2).PadLeft(36, '0')}");
                    Console.WriteLine($"To zero:      {Convert.ToString(zero, 2).PadLeft(36, '0')}");
                    continue;
                }

                // Parse
                var parsed = regex.Match(item);
                var mem = int.Parse(parsed.Groups["mem"].Value);
                var value = int.Parse(parsed.Groups["value"].Value);
                Console.WriteLine($"Value :  {Convert.ToString(value, 2).PadLeft(36, '0')}");
                Console.WriteLine($"Becomes: {Convert.ToString((value & ~zero) | ones, 2).PadLeft(36,'0')}");
                output[mem] = (value & ~zero) | ones;

            }
            return output.Select(i => i.Value).Sum();
        }

        private static long Test2(List<string> input)
        {
            var masks = new List<long>();
            long zeros = 0;
            long ones = 0;

            var regex = new Regex(@"^mem\[(?<mem>\d+)\]\s=\s(?<value>\d+)$");
            var output = new Dictionary<long, long>();
            foreach (var item in input)
            {
                if (item.StartsWith("mask"))
                {
                    List<long> memOffsets = new List<long>();
                    ones = 0;
                    zeros = 0;
                    var mask = item.Split("= ")[1];
                    Console.WriteLine($"Changing mask:   {mask}");
                    foreach (var maskItem in mask.Reverse().Select((value, i) => (value, i)))
                    {
                        if (maskItem.value == '1')
                        {
                            ones |= (long)Math.Pow(2, maskItem.i);
                        }
                        if (maskItem.value == 'X')
                        {
                            zeros |= (long)Math.Pow(2, maskItem.i);
                            memOffsets.Add((long)Math.Pow(2, maskItem.i));
                        }
                    }
                    masks = Enumerable.Range(0, 1 << (memOffsets.Count))
                                          .Select(index => 
                                                       memOffsets.Where((v, i) => (index & (1 << i)) != 0).Sum()
                                            ).ToList();
                    continue;
                }

                // Parse
                var parsed = regex.Match(item);
                Console.WriteLine($"Changing parsed: {Convert.ToString(long.Parse(parsed.Groups["mem"].Value), 2).PadLeft(36, '0')}");
                var mem = (long.Parse(parsed.Groups["mem"].Value) & (~zeros & 0xFFFFFFFFF)) | ones ;
                Console.WriteLine($"Changing mem   : {Convert.ToString(((long.Parse(parsed.Groups["mem"].Value) & (~zeros & 0xFFFFFFFFF)) | ones), 2).PadLeft(36, '0')}");
                var value = long.Parse(parsed.Groups["value"].Value) ;
                foreach(var mask in masks)
                {
                    //Console.WriteLine($"Changing mem-  : {Convert.ToString(mem + mask, 2).PadLeft(36, '0')}");
                    output[mem + mask] = value;
                }
            }
            return output.Select(i => i.Value).Sum();
        }
    }
}
