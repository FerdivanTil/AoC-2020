using System;
using System.Collections.Generic;
using System.Text;

namespace Aoc.Day12
{
    public class Navigator1
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Direction { get; set; } = 90;
        protected Dictionary<string, Action<int>> Actions { get; set; }
        public Navigator1()
        {
            Actions = new Dictionary<string, Action<int>>
            {
                { "N", (i) => { Y += i; }},
                { "E", (i) => { X += i; }},
                { "S", (i) => { Y -= i; }},
                { "W", (i) => { X -= i; }},
                { "L", (i) => { Direction = (Direction + 360 - i) % 360; }},
                { "R", (i) => { Direction = (Direction + i) % 360; }},
            };
        }
        public void Goto(string undecodedAction)
        {
            var action = undecodedAction.Substring(0, 1);
            var amount = int.Parse(undecodedAction.Substring(1, undecodedAction.Length - 1));
            if (action == "F")
            {
                action = GetDirectionAction();
            }
            Actions[action](amount);
        }
        public string GetDirectionAction()
        {
            var directions = new[] { "N", "E", "S", "W" };
            return directions[Direction / 90];
        }

        public int GetDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);

        }
    }
}
