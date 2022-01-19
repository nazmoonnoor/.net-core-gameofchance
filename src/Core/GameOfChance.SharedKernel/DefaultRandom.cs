using GameOfChance.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.SharedKernel
{
    public class DefaultRandom : IRandomGenerator
    {
        public int Generate(int max)
        {
            return new Random().Next(0, max);
        }
    }
}
