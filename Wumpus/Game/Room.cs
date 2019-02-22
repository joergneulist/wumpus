using System;
using System.Linq;

namespace Wumpus.Game
{
    class Room
    {
        public bool HasBat { get; set; }
        public bool HasPit { get; set; }
        public int[] Neighbours { get; private set; }
        public int Number { get; set; }

        public Room(int number, int tunnelCount)
        {
            Number = number;
            HasBat = false;
            HasPit = false;
            Neighbours = Enumerable.Repeat(-1, tunnelCount).ToArray();
        }

        public int FindFirstUnlinkedTunnel()
        {
            return Array.IndexOf(Neighbours, -1);
        }

        public override string ToString()
        {
            var output = Number + ":" + string.Join(",", Neighbours);
            if (HasBat) output += ",B";
            if (HasPit) output += ",P";
            return output;
        }
    }
}
