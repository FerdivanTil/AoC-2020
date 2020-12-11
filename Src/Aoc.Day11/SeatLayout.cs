using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoc.Day11
{
    public class SeatLayout
    {
        public List<List<char>> Input { get; set; }

        public List<List<State>> Occupied { get; set; }

        public List<List<State>> RunningOccupied { get; set; }

        public int xCount { get => Input[0].Count; }

        public int yCount { get => Input.Count; }
        public Wave CurrentWave { get;set; } = Wave.Arriving;

        public SeatLayout(List<List<char>> input)
        {
            Input = input;
            Initialize();
        }

        public void CopyRunningToOccupieed()
        {
            foreach (var y in Enumerable.Range(0, yCount))
            {
                foreach (var x in Enumerable.Range(0, xCount))
                {
                    Occupied[y][x] = RunningOccupied[y][x];
                }
            }
        }
        public void Initialize()
        {
            var item = new List<List<State>>();
            foreach (var y in Enumerable.Range(0, yCount))
            {
                var row = new List<State>();
                foreach (var x in Enumerable.Range(0, xCount))
                {
                    row.Add(State.Free);
                }
                item.Add(row);
            }
            Occupied = item;

            item = new List<List<State>>();
            foreach (var y in Enumerable.Range(0, yCount))
            {
                var row = new List<State>();
                foreach (var x in Enumerable.Range(0, xCount))
                {
                    row.Add(State.Free);
                }
                item.Add(row);
            }
            RunningOccupied = item;
        }

        public string GetState()
        {
            var output = string.Empty;
            foreach (var y in Enumerable.Range(0, yCount))
            {
                foreach (var x in Enumerable.Range(0, xCount))
                {
                    if(Input[y][x] == '.')
                    {
                        output += '.';
                        continue;
                    }
                    if(Occupied[y][x] == State.Occupied)
                    {
                        output += '#';
                        continue;
                    }
                    output += 'L';
                }
                output += Environment.NewLine;
            }
            return output;
        }
        public bool Start(int occupiedSeatsAdjacentCount)
        {
            var isChanged = false;
            foreach(var y in Enumerable.Range(0, yCount))
            {
                foreach(var x in Enumerable.Range(0, xCount))
                {
                    if (IsFloor(x, y))
                        continue;
                    if(CurrentWave == Wave.Arriving)
                    {
                        if(GetState(x,y) == State.Occupied)
                        {
                            continue;
                        }
                        if(HasOccupiedSeatsAdjacent2(x,y) == 0)
                        {
                            isChanged = true;
                            RunningOccupied[y][x] = State.Occupied;
                        }
                    }
                    if (CurrentWave == Wave.Leaving)
                    {
                        if (GetState(x, y) == State.Free)
                        {
                            continue;
                        }
                        if (HasOccupiedSeatsAdjacent2(x, y) >= occupiedSeatsAdjacentCount)
                        {
                            isChanged = true;
                            RunningOccupied[y][x] = State.Free;
                        }
                    }
                }
            }
            CurrentWave = CurrentWave == Wave.Leaving ? Wave.Arriving : Wave.Leaving;
            CopyRunningToOccupieed();
            return isChanged;
        }

        public int GetOccupiedCount()
        {
            var i = 0;
            foreach (var y in Enumerable.Range(0, yCount))
            {
                foreach (var x in Enumerable.Range(0, xCount))
                {
                    if (Input[y][x] == 'L' && Occupied[y][x] == State.Occupied)
                        i++;
                }
            }
            return i;
        }

        public bool IsFloor(int x, int y)
        {
            return Input[y][x] == '.';
        }

        public State GetState(int x, int y)
        {
            return Occupied[y][x];
        }
        public int HasOccupiedSeatsAdjacent1(int x, int y)
        {
            var count = 0;
            
            // Top
            if (y != 0 && GetState(x, y - 1) == State.Occupied)
                count++;
            // TopLeft
            if (y != 0 && x < xCount -1 &&  GetState(x + 1, y - 1) == State.Occupied)
                count++;
            // Left
            if (x < xCount -1 && GetState(x + 1, y) == State.Occupied)
                count++;
            // BottomRight
            if (x < xCount -1 && y < yCount -1 && GetState(x + 1, y +1) == State.Occupied)
                count++;
            // Bottom
            if ( y < yCount -1 && GetState(x, y + 1) == State.Occupied)
                count++;
            // BottomLeft
            if (x != 0 && y < yCount -1 && GetState(x - 1, y + 1) == State.Occupied)
                count++;
            // Left
            if (x != 0 && GetState(x - 1, y) == State.Occupied)
                count++;
            // TopLeft
            if (x != 0 && y != 0 && GetState(x - 1, y -1) == State.Occupied)
                count++;
            return count;
        }
        public int HasOccupiedSeatsAdjacent2(int x, int y)
        {
            var count = 0;

            // Top
            if (y != 0)
            {
                foreach(var i in Enumerable.Range(1,y))
                {
                    if (Input[y - i][x] == '.')
                        continue;
                    if(GetState(x, y - i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // TopRight
            if (y != 0 && x < xCount - 1)
            {
                foreach (var i in Enumerable.Range(1, y))
                {
                    if (x + i >= xCount)
                        break;
                    if (Input[y - i][x + i] == '.')
                        continue;
                    if (GetState(x + i, y - i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            // Right
            if (x < xCount - 1)
            {
                foreach (var i in Enumerable.Range(1, xCount - x -1))
                {
                    if (Input[y][x + i] == '.')
                        continue;
                    if (GetState(x + i, y) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            // BottomRight
            if (x < xCount - 1 && y < yCount - 1)
            {
                foreach (var i in Enumerable.Range(1, yCount - y -1))
                {
                    if (x + i >= xCount)
                        break;
                    if (Input[y + i][x + i] == '.')
                        continue;
                    if (GetState(x + i, y + i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }


            // Bottom
            if (y < yCount - 1)
            {
                foreach (var i in Enumerable.Range(1, yCount - y -1))
                {
                    if (Input[y + i][x] == '.')
                        continue;
                    if (GetState(x, y + i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // BottomLeft
            if (x != 0 && y < yCount - 1)
            {
                foreach (var i in Enumerable.Range(1, yCount - y -1))
                {
                    if (x -i < 0)
                        break;
                    if (Input[y + i][x - i] == '.')
                        continue;
                    if (GetState(x - i, y + i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Left
            if (x != 0 )
            {
                foreach (var i in Enumerable.Range(1, x))
                {
                    if (Input[y][x - i] == '.')
                        continue;
                    if (GetState(x - i, y) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            // TopLeft
            if (x != 0 && y != 0)
            {
                foreach (var i in Enumerable.Range(1, y))
                {
                    if (x - i < 0)
                        break;
                    if (Input[y - i][x - i] == '.')
                        continue;
                    if (GetState(x - i, y - i) == State.Occupied)
                    {
                        count++;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return count;
        }
    }
    public enum State
    {
        Free = 0,
        Occupied = 1,
    }
    public enum Wave
    {
        Arriving = 0,
        Leaving = 1,
    }
}
