using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aoc.Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = System.IO.File.ReadAllText(@"input-day-4-test.txt");

            text = System.IO.File.ReadAllText(@"input-day-4.txt");
            var input = Regex.Split(text,"\r\n\r\n",RegexOptions.Multiline).ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} valids.");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} valids.");

            Console.WriteLine("Done processing.");
        }
        private static int Test1(List<string> input)
        {
            var i = 0;
            // Parse the input
            var parsedInput = Parse(input);

            foreach(var item in parsedInput)
            {
                // Check if the input is valid
                if (IsValidTest1(item))
                {
                    i++;
                }
            }
            return i;
        }
        private static int Test2(List<string> input)
        {
            var i = 0;
            // Parse the input
            var parsedInput = Parse(input);

            foreach (var item in parsedInput)
            {
                // Check if the input is valid
                if (IsValidTest2(item))
                {
                    i++;
                }
            }
            return i;
        }

        private static IEnumerable<Dictionary<string, string>> Parse(List<string> input)
        {
            var pattern = @"^(?:(?<k>[a-z]{3}):(?<v>[^\s]+)\r?\s?)*$";
            var regex = new Regex(pattern, RegexOptions.Multiline);
            var i = 0;
            foreach (var item in input)
            {
                var match = regex.Match(item);
                if (!match.Success)
                {
                    Console.WriteLine($"Error on input text: {input}");
                    yield return null;
                }
                var keys = match.Groups["k"].Captures.Select(i => i.Value);
                var values = match.Groups["v"].Captures.Select(i => i.Value);
                yield return keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            }
        }
        private static bool IsValidTest1(Dictionary<string,string> input)
        {
            var requiredKeys = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            return input.Keys.Intersect(requiredKeys).Count() == requiredKeys.Count();
        }
        private static bool IsValidTest2(Dictionary<string, string> input)
        {
            var validators = new[] { 
                new { key =  "byr",  regex = "^[0-9]{4}$", expr = new Func<string,bool>(i => int.Parse(i)>=1920 && int.Parse(i)<=2002 )}, 
                new { key = "iyr" ,  regex = "^[0-9]{4}$", expr = new Func<string,bool>(i => int.Parse(i)>=2010 && int.Parse(i)<=2020 ) }, 
                new { key = "eyr" ,  regex = "^[0-9]{4}$", expr = new Func<string,bool>(i => int.Parse(i)>=2020 && int.Parse(i)<=2030 ) }, 
                new { key = "hgt" , regex = "^([0-9]{3}cm|[0-9]{2}in)$", expr = new Func<string,bool>(i => {if(i[2] == 'i')return int.Parse(i.Substring(0,2))>=59 && int.Parse(i.Substring(0,2))<=76;  else return int.Parse(i.Substring(0,3))>=150 && int.Parse(i.Substring(0,3))<=193; }) }, 
                new { key = "hcl" ,  regex = "^#[0-9a-z]{6}", expr = new Func<string,bool>(i => true ) }, 
                new { key = "ecl" ,  regex = "^(amb|blu|brn|gry|grn|hzl|oth)$", expr = new Func<string,bool>(i => true ) }, 
                new { key = "pid" ,  regex = "^[0-9]{9}$", expr = new Func<string,bool>(i => true )} 
            };

            // Check al required keys
            if(input.Keys.Intersect(validators.Select(i => i.key)).Count() != validators.Count())
            {
                return false;
            }

            // Validate each field.
            foreach(var item in validators)
            {
                // Get the value
                var value = input.GetValueOrDefault(item.key);

                // Check if the value is valid.
                if(!Regex.IsMatch(value, item.regex) || !item.expr(value))
                    return false;
            }
            return true;
        }
        
    }
}
