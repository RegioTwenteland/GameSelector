using System;

namespace GameSelector.Model
{
    internal class RandomNumberGenerator : IRandomNumberGenerator
    {
        private static readonly Random rng = new Random();

        public int Next(int max)
        {
            return rng.Next(max);
        }
    }
}
