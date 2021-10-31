using System;

namespace MazeSolver
{
    internal class Step
    {
        public Step()
        {
        }
        public Tuple<int, int> Coordinate { get; set; }
        public double DistanceFromEnd { get; set; }
    }
}