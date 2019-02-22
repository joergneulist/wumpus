using System;
using System.Collections.Generic;
using System.Text;

namespace Wumpus
{
    public class Math
    {
        private Random rand = new Random();

        public int GreatestCommonDivisor(int a, int b)
        {
            if (a < b)
                return GreatestCommonDivisor(b, a);

            int remainder = a % b;

            if (remainder == 0)
                return b;

            return GreatestCommonDivisor(b, remainder);
        }

        public int Roll(int dieFaces)
        {
            return rand.Next(0, dieFaces);
        }

        public bool IsOneInNChance(int dieFaces)
        {
            return Roll(dieFaces) == 0;
        }
    }
}
