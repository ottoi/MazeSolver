using System;
using System.Collections.Generic;
using System.IO;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxIterations = new int[] { 20, 150, 200 };
            var mapPathFirst = "maze-task-first.txt";
            var mapPathSecond = "maze-task-second.txt";
            var resultFilePathFirst = "maze_result_first";
            var resultFilePathSecond = "maze_result_second";

            SolveWith(mapPathFirst, resultFilePathFirst, maxIterations, new TurnLeftSolver());
            SolveWith(mapPathFirst, resultFilePathFirst, maxIterations, new WeightedSolver());
            SolveWith(mapPathSecond, resultFilePathSecond, maxIterations, new TurnLeftSolver());
            SolveWith(mapPathSecond, resultFilePathSecond, maxIterations, new WeightedSolver());
        }

        private static void SolveWith(string mapPath, string resultPath, int[] maxIterations, ISolverAlgorithm solverAlgorithm)
        {
            foreach (var maxIteration in maxIterations)
            {
                var resultFilePath = $"{resultPath}_{solverAlgorithm.GetType()}_{maxIteration}.txt";

                Console.WriteLine("Start solving the maze");
                var solver = new MazeApp(mapPath, resultFilePath, maxIteration);
                solver.Options.SolverAlgorithm = solverAlgorithm;
                var numberOfIterations = solver.Solve();

                Console.WriteLine($"Solving ended. Number of iterations {numberOfIterations}.");
            }
        }
    }
}
