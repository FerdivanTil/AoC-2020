using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "..##.......",
                                            "#...#...#..",
                                            ".#....#..#.",
                                            "..#.#...#.#",
                                            ".#...##..#.",
                                            "..#.##.....",
                                            ".#.#.#....#",
                                            ".#........#",
                                            "#.##...#...",
                                            "#...##....#",
                                            ".#..#...#.#" };

            var text = System.IO.File.ReadAllText(@"input-day-3.txt");
            input = text.Split(Environment.NewLine).ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} trees.");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} trees.");

            Console.WriteLine("Done processing.");

        }
        private static int Test1(List<string> input)
        {
            var i = 1;
            var trees = 0;
            foreach (var line in input.Skip(1))
            {
                if (line[(i * 3) % line.Count()] == '#')
                {
                    trees++;
                }
                i++;
            }
            return trees;
        }
        private static int Test2(List<string> input)
        {
            var slopes = new[]
            {
                new { x = 1, y = 1 },
                new { x = 3, y = 1 },
                new { x = 5, y = 1 },
                new { x = 7, y = 1},
                new { x = 1, y = 2 },
            };
            int returnValue = 1;
            foreach(var slope in slopes)
            {
                var i = 1;
                var trees = 0;
                foreach (var line in input.Where((value, index) => index % slope.y == 0).Skip(1))
                {
                    if (line[(((i * slope.x)) % line.Count())] == '#')
                    {
                        trees++;
                    }
                    i++;
                }
                returnValue *= trees;
                Console.WriteLine($"Slope {slope.x}-{slope.y} returns {trees} trees.");

            }
            return returnValue;
        }
    }
}
