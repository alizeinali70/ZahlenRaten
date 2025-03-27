using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZahlenRaten.Interface;

namespace ZahlenRaten.Repositories
{
    public class RandomNumberGenerator : INumberGenerator
    {
        private static readonly Random _random = new Random();
        public int GenerateNumber(int min, int max) => _random.Next(min, max + 1);
    }
}
