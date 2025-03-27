using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZahlenRaten.Interface
{
    public interface INumberGenerator
    {
        int GenerateNumber(int min, int max);
    }
}
