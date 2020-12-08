using Aoc.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new List<string> { "nop +0",
                                            "acc +1",
                                            "jmp +4",
                                            "acc +3",
                                            "jmp -3",
                                            "acc -99",
                                            "acc +1",
                                            "jmp -4",
                                            "acc +6"};

            var text = System.IO.File.ReadAllText(@"input-day-8.txt");
            input = text.Split("\r\n").ToList();
            var i = Test1(input);

            Console.WriteLine($"Test1: Found {i} as acc");

            i = Test2(input);

            Console.WriteLine($"Test2: Found {i} as acc");

            Console.WriteLine("Done processing.");
        }
        private static int Test1(List<string> input)
        {
            var returnValue = 0;
            var proc = new Processor();
            proc.Load(input);
            var visited = new List<int>();
            Func<int, int, bool> trace = (acc, ptr) =>
              {
                  if (visited.Contains(ptr))
                  {
                      returnValue = acc;
                      return false;
                  }

                  visited.Add(ptr);
                  return true;
              };
            proc.Trace = trace;
            proc.Execute();
            return returnValue;
        }

        private static int Test2(List<string> input)
        {
            var returnValue = 0;
            var proc = new Processor();
            proc.Load(input);

            // Keep a list of all visited lines.
            var visited = new List<int>();
            Func<int, int, bool> trace = (acc, ptr) =>
            {
                // Always save the acc. It might be the last one of the program.
                returnValue = acc;
                if (visited.Contains(ptr))
                {
                    return false;
                }

                visited.Add(ptr);
                return true;
            };
            proc.Trace = trace;

            // Prepare the repair loop.
            var step = 1;
            var lastChange = 0;
            var hasFixed = false;
            Func<string, string> inject = (command) =>
            {
                // if we already fixed the program then we cannot fix it again.
                if (hasFixed)
                {
                    return command;
                }
                // Do nothing if the command is fixed before.
                if (step <= lastChange)
                {
                    step++;
                    return command;
                }
                
                // Change the command and see if this works
                if (command == "nop")
                {
                    hasFixed = true;
                    lastChange = step;
                    return "jmp";
                }
                if (command == "jmp")
                {
                    hasFixed = true;
                    lastChange = step;
                    return "nop";
                }
                return command;
            };
            // Enable the injection of commands.
            proc.Inject = inject;

            // Keep trying until we have fixed the correct command.
            var tryAgaing = true;
            while (tryAgaing)
            {
                // Clear all values for a new try http://gph.is/2beiqKq
                visited.Clear();
                returnValue = 0;
                hasFixed = false;
                step = 1;
                // Lets try again!
                proc.Reset();
                tryAgaing = !proc.Execute();
            }
            return returnValue;
        }
    }
}
