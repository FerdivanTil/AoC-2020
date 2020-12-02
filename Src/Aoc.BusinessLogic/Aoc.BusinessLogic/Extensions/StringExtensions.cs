using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static int CountChar(this string input, char character)
        {
            var count = 0;
            foreach (var c in input)
            {
                if (c == character)
                    count++;
            }
            return count;
        }
    }
}
