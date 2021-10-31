using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeSolver
{
    public class WeightedSolver : ISolverAlgorithm
    {
        /// <summary>
        /// WeightedSolver solves the maze by finding the minimum distance to the exit with every move.
        /// In every iteration player moves to the adjacent element
        /// The distance from the element to the exit is weighted respect to the element type
        /// Exit elements ("E") has the weight of 0
        /// Wall elements ("#") has the weight of infinity
        /// Clear elements (" ") has the weight of 1
        /// If element is already visited the weight is n * 10 where n is the number of times visited in the element
        /// </summary>
        public WeightedSolver()
        {
        }

        public void MovePlayer(Player player, Map map)
        {
           var possibleSteps = GetPossibleSteps(player, map);
            possibleSteps = AddWeightIfElementIsAlreadyVisited(possibleSteps, map);
            MakeTheMove(possibleSteps,player,map);     
        }


        private void MakeTheMove(IList<Step> possibleSteps, Player player, Map map)
        {
            foreach (var s in possibleSteps)
            {
                var element = map.GetElementAt(s.Coordinate);

                switch (element)
                {
                    case "E":
                        player.Position = s.Coordinate;
                        player.Path.Add(s.Coordinate);
                        return;
                    case " ":
                        player.Position = s.Coordinate;
                        player.Path.Add(s.Coordinate);
                        map.SetElementAt(player.Position, "1");
                        return;
                    case string elem when int.TryParse(elem, out var nTimesInElement):
                        player.Position = s.Coordinate;
                        player.Path.Add(s.Coordinate); ;
                        map.SetElementAt(player.Position, $"{nTimesInElement + 1}");
                        return;
                }
                player.Path.Add(player.Position);
            }
        }
        private IList<Step> GetPossibleSteps(Player player, Map map)
        {
            var possibleSteps = new List<Step>();
            // Left
            possibleSteps.Add(GetPossibleStep(player.Position.Item1, player.Position.Item2 - 1, map));
            // Right
            possibleSteps.Add(GetPossibleStep(player.Position.Item1, player.Position.Item2 + 1, map));
            // Down
            possibleSteps.Add(GetPossibleStep(player.Position.Item1 - 1, player.Position.Item2, map));
            // Up
            possibleSteps.Add(GetPossibleStep(player.Position.Item1 + 1, player.Position.Item2, map));

            possibleSteps.RemoveAll(x => x == null);
            return possibleSteps;
        }

        private Step GetPossibleStep(int x, int y, Map map)
        {
            if ((x >= 0 && x <= map.xMax) && (y >= 0 && y <= map.yMax))
            {
                var step = new Step();
                step.Coordinate = new Tuple<int, int>(x, y);
                step.DistanceFromEnd = GetClosestEndPoint(step.Coordinate, map.EndPoints);
                return step;
            }

            return null;
        }

        private IList<Step> AddWeightIfElementIsAlreadyVisited(IList<Step> possibleSteps,Map map)
        {
            foreach (var step in possibleSteps)
            {
                var elem = map.GetElementAt(step.Coordinate);
                if (int.TryParse(elem.ToString(), out var i))
                {
                    step.DistanceFromEnd = (i * 10) * step.DistanceFromEnd;
                }
            }

            return possibleSteps.OrderBy(s => s.DistanceFromEnd).ToList();
        }

        private double GetClosestEndPoint(Tuple<int, int> coordinate, List<Tuple<int, int>> endPoints)
        {
            var result = new List<double>();
            foreach (var endPoint in endPoints)
            {
                var d = Math.Sqrt(Math.Pow(coordinate.Item1 - endPoint.Item1, 2) + Math.Pow(coordinate.Item2 - endPoint.Item2, 2));
                result.Add(d);
            }

            // Order result list from shortest distance to farthest 
            result.OrderBy(d => d);
            if (result == null || result.Count == 0)
            {
                throw new ApplicationException("Closest end point not found");
            }
            return result.First();
        }

    }
}