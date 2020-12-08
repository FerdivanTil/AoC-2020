using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.BusinessLogic
{
    public class Processor
    {
        protected int RegA { get; set; }
        protected Dictionary<string,Action> Instructions { get; set; }
        protected int Acc { get; set; }
        protected int Pointer { get; set; } = 0;
        protected List<string> Memory { get; set; }

        public Processor()
        {
            Instructions = new Dictionary<string, Action>
            {
                { "nop", () => { Pointer++; }},
                { "acc", () => { Acc += RegA; Pointer++; } },
                { "jmp", () => { Pointer += RegA; } }
            };
        }
        public void Load(IEnumerable<string> input)
        {
            Memory = input.ToList();
        }
        public Func<int,int,bool> Trace { get; set; }

        public Func<string, string> Inject { get; set; }

        protected Command Decode(string input)
        {
            var parsed = input.Split(' ');
            return new Command(parsed[0], int.Parse(parsed[1]));
        }
        public void Reset()
        {
            Pointer = 0;
            Acc = 0;
            RegA = 0;
        }
        public bool Execute()
        {
            while(true)
            {
                if(Pointer >= Memory.Count)
                {
                    return true;
                }
                // Decode command
                var command = Decode(Memory[Pointer]);


                // Load value in register
                RegA = command.Value;

                if(Inject != null)
                {
                    command.Instruction = Inject(command.Instruction);
                }

                // Get instuction
                var instruction = Instructions[command.Instruction];

                // Execute instuction
                instruction();
                // Send trace
                if (Trace == null)
                {
                    continue;
                }
                var shouldContinue = Trace(Acc, Pointer);
                if (!shouldContinue)
                {
                    return false;
                }
            }
            
        }
        protected class Command
        {
            public Command(string instruction, int value)
            {
                Instruction = instruction;
                Value = value;
            }
            public string Instruction { get; set; }
            public int Value { get; set; }
        }

    }
}
