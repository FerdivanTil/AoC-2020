using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL" };

            var text = System.IO.File.ReadAllText(@"input-day-5.txt");
            input = text.Split(Environment.NewLine).ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as highest seat");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} as my seat");

            Console.WriteLine("Done processing.");
        }
        private static IEnumerable<int> GetSeats(List<string> input)
        {
            foreach (var item in input)
            {
                // Take the first part:
                var bin = item.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');
                
                // Just for reference
                var row = Convert.ToInt32(bin.Substring(0, 7), 2);
                var col = Convert.ToInt32(bin.Substring(7, 3), 2);
                
                // The actual seat number
                var seat = Convert.ToInt32(bin, 2);

                Console.WriteLine($"item is in row {row} col {col} seat {seat}");
                yield return seat;
            }
        }
        private static int Test1(List<string> input)
        {
            return GetSeats(input).Max();
        }

        private static int Test2(List<string> input)
        {
            var seats = GetSeats(input).OrderBy(i => i).ToList();
            var should = Enumerable.Range(seats.First(), seats.Count()+1);

            var mySeat = should.Sum() - seats.Sum();
            Console.WriteLine($"My seat is: {mySeat}");
            return mySeat;
        }
    }
}
