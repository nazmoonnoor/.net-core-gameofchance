using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.SharedKernel.Interfaces
{
    public interface IRandomGenerator
    {
        int Generate(int max);
    }
}
