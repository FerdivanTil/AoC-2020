using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Day12
{
    public class Navigator2
    {
        public int wayPointX { get; set; } = 10;
        public int wayPointY { get; set; } = 1;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Direction { get; set; } = 90;
        protected Dictionary<string, Action<int>> Actions { get; set; }
        public Navigator2()
        {
            Actions = new Dictionary<string, Action<int>>
            {
                { "N", (i) => { wayPointY += i; }},
                { "E", (i) => { wayPointX += i; }},
                { "S", (i) => { wayPointY -= i; }},
                { "W", (i) => { wayPointX -= i; }},
                { "F", (i) => { X += wayPointX * i; Y += wayPointY * i; } }
            };
        }
        public void Goto(string undecodedAction)
        {
            var action = undecodedAction.Substring(0, 1);
            var amount = int.Parse(undecodedAction.Substring(1, undecodedAction.Length - 1));
            if (action == "L" || action == "R")
            {
                ChangeDirection(action,amount);
                return;
            }
            Actions[action](amount);
        }
        public void ChangeDirection(string action, int amount)
        {
            if(action == "R")
            {
                foreach(var i in Enumerable.Range(0, (amount / 90)))
                {
                    var tempX = wayPointX;
                    wayPointX = wayPointY;
                    wayPointY = tempX * -1;
                }
            }

            if (action == "L")
            {
                foreach (var i in Enumerable.Range(0, (amount / 90)))
                {
                    var tempX = wayPointX;
                    wayPointX = wayPointY * -1;
                    wayPointY = tempX;
                }
            }
        }

        public int GetDistance()
        {
            return Math.Abs(X) + Math.Abs(Y);

        }
    }
}
