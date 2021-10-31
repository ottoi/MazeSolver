using System;
using System.IO;

namespace MazeSolver
{
    public class MazeApp
    {
        private Map _map;
        private int _maxIterations;
        private Player _player;
        private string _resultFilePath; 

        public MazeApp(string mapPath, string resultFilePath, int maxNumberofIterations)
        {
            _resultFilePath = resultFilePath;
            _maxIterations = maxNumberofIterations;

            LoadMap(mapPath);
            _map.Initialize();
            InitializePlayer();

            Options = new MazeAppOptions()
            {
                SolverAlgorithm = new TurnLeftSolver(), //new WeightedSolver(),
                MaxNumberofIterations = _maxIterations
            };
        }

        public MazeAppOptions Options { get; set; }

        public int Solve()
        {

            ISolverAlgorithm solver = Options.SolverAlgorithm;
            if(_map == null)
            {
                throw new ArgumentException("Map is not loaded. Call Initialize in order to load it.");
            }

            var nIterations = 0;
            while (!_map.EndPoints.Contains(_player.Position) && nIterations < Options.MaxNumberofIterations)
            {
                solver.MovePlayer(_player, _map);

                Console.WriteLine($"Position: ({_player.Position.Item1},{_player.Position.Item2})");
                nIterations++;
            }

            File.WriteAllText(_resultFilePath, _map.Show());
            return nIterations;
        }

        private void LoadMap(string path)
        {
            var p = Path.GetFullPath(path);
            var mapString = File.ReadAllText(p);
            _map = new Map(mapString);
        }

        private void InitializePlayer()
        {
            _player = new Player();
            _player.Position = _map.StartingPoint;
            _map.SetElementAt(_player.Position, "1");
        }
    }
}
