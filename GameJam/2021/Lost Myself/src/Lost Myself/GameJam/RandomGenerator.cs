using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    static class RandomGenerator
    {
        private static Random rand;

        public static void Init(int seed=0)
        {
            if(seed == 0)
            {
                rand = new Random();
            }
            else
            {
                rand = new Random(seed);
            }
        }

        public static int GetRandomInt(int max, int min = 0)
        {
            return rand.Next(min, max);
        }

        public static double GetRandomDouble()
        {
            return rand.NextDouble();
        }

        public static double GetRandomDouble(double max, double min = 0)
        {
            double f = rand.NextDouble();
            f *= max - min;
            f += min;
            return f;
        }
    }
}
