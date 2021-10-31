using System;

namespace MazeSolver
{
    public class TurnLeftSolver : ISolverAlgorithm
    {
        private Movement _previousMoveDirection;

        /// <summary>
        /// TurnLeftSolver is a dummy solver which tries to solve the maze by going straight until the player encounters the wall ("#"). 
        /// When the wall is encountered the player takes left turns until it can go straight.
        /// </summary>
        public TurnLeftSolver()
        {

        }

        public void MovePlayer(Player player, Map map)
        {
            var isMoveTaken = false;
            // Take first step
            if (_previousMoveDirection == Movement.None)
            {
                _previousMoveDirection = Movement.Up;
            }

            while(!isMoveTaken)
            {
                var nextCoordinate = _previousMoveDirection switch
                {
                    Movement.Up => new Tuple<int, int>(player.Position.Item1, player.Position.Item2 - 1),
                    Movement.Down => new Tuple<int, int>(player.Position.Item1, player.Position.Item2 + 1),
                    Movement.Left => new Tuple<int, int>(player.Position.Item1 - 1, player.Position.Item2),
                    Movement.Right => new Tuple<int, int>(player.Position.Item1 + 1, player.Position.Item2),
                    _ => null
                };

                if (CanCotinueToCoordinates(nextCoordinate, map))
                {
                    player.Position = nextCoordinate;
                    map.SetElementAt(player.Position, "1");
                    isMoveTaken = true;
                }
                else
                {
                    TurnLeft();
                }              
            }


        }

        private void TurnLeft()
        {
            _ = _previousMoveDirection switch
            {
                Movement.Up => _previousMoveDirection = Movement.Left,
                Movement.Down => _previousMoveDirection = Movement.Right,
                Movement.Left => _previousMoveDirection = Movement.Down,
                Movement.Right => _previousMoveDirection = Movement.Up,
                _ => Movement.None
            };
        }

        private bool CanCotinueToCoordinates(Tuple<int, int> coordinate, Map map)
        {
            if (coordinate == null) return false;
            return !(map.GetElementAt(coordinate) == "#");
        }

    }

}



