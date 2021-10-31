using System;

namespace MazeSolver
{
    public class State
    {
        public Tuple<int, int> Coordinate { get; set; }
        public double DistanceFromEnd { get; set; }
    }
}