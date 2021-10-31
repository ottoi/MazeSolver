using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolver
{
    public class Player
    {
        public Player()
        {
            Path = new List<Tuple<int, int>>();
        }

        public Tuple<int,int> Position;

        public List<Tuple<int, int>> Path;
    }
}
