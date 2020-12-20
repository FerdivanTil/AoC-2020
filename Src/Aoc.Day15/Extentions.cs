using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Day15
{
    public static class Extentions
    {
        public static void AddOrSet(this Dictionary<int,Number> self, int number, int index)
        {
            var suc6 = self.TryGetValue(number, out var obj);
            if(suc6)
            {
                obj.AddIndex(index);
                return;
            }
            self.Add(number, new Number(number, index));
        }
    }
}
