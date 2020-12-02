using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Aoc.BusinessLogic.Extensions;

namespace Aoc.Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };

            var text = System.IO.File.ReadAllText(@"input-day-2.txt");
            input = text.Split(Environment.NewLine).ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} VALID passwords.");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} VALID passwords.");

            Console.WriteLine("Done processing.");
        }
        private static int Test1(List<string> input)
        {
            var pattern = @"^(?<min>\d{1,2})-(?<max>\d{1,2})\s(?<char>[a-z]{1}):\s(?<password>[a-z]*)$";
            var regex = new Regex(pattern);
            var i = 0;
            foreach (var item in input)
            {
                var match = regex.Match(item);
                if (!match.Success)
                {
                    Console.WriteLine($"Error on input text: {input}");
                    return 0;
                }
                var password = match.Groups["password"].Value;
                var character = match.Groups["char"].Value[0];
                var min = int.Parse(match.Groups["min"].Value);
                var max = int.Parse(match.Groups["max"].Value);
                var charCount = password.CountChar(character);
                if (charCount > max || charCount < min)
                {

                    Console.WriteLine($"Incorrect password found: {password}");
                    continue;
                }
                i++;
            }
            return i;
        }

        private static int Test2(List<string> input)
        {
            var pattern = @"^(?<min>\d{1,2})-(?<max>\d{1,2})\s(?<char>[a-z]{1}):\s(?<password>[a-z]*)$";
            var regex = new Regex(pattern);
            var i = 0;
            foreach (var item in input)
            {
                var match = regex.Match(item);
                if (!match.Success)
                {
                    Console.WriteLine($"Error on input text: {input}");
                    return 0;
                }
                var password = match.Groups["password"].Value;
                var character = match.Groups["char"].Value[0];
                var min = int.Parse(match.Groups["min"].Value);
                var max = int.Parse(match.Groups["max"].Value);
                //var charCount = password.CountChar(character);
                if (password[min - 1] == character && password[max - 1] == character)
                {
                    Console.WriteLine($"Incorrect password found: {item}");
                    continue;
                }
                if (password[min - 1] != character && password[max - 1] != character)
                {
                    Console.WriteLine($"Incorrect password found: {item}");
                    continue;
                }

                i++;
            }
            return i;
        }
    }
}
