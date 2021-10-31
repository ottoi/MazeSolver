using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MazeSolver
{
    public class Map
    {
        private readonly string _mapString = null;
        private Dictionary<Tuple<int,int>,string> _mapDictionary = null;

        public Map(string mapString)
        {
            _mapString = mapString;
            EndPoints = new List<Tuple<int, int>>();
        }

        public Tuple<int, int> StartingPoint { get; set; }
        public  List<Tuple<int, int>> EndPoints { get; set; }

        public int xMax { get; set; }
        public int yMax { get; set; }
        public void Initialize()
        {
            int i = 0, j = 0;
            var map = new Dictionary<Tuple<int,int>, string>();
            IEnumerable<string> mapLineArray = _mapString.Split('\n');

            // Hakkerointi rivi
            mapLineArray = mapLineArray.Take(mapLineArray.Count() - 1);
            yMax = mapLineArray.Count()-1;
            foreach (var row in mapLineArray)
            {
                xMax = row.Length -1 > xMax ? row.Length -1 : xMax;
                j = 0;
                var rowDictionary = new Dictionary<int, string>();
                foreach (var col in row.Trim())
                {
                    if(col.Equals('^'))
                    {
                        StartingPoint = new Tuple<int, int>(j, i);
                    }
                    if (col.Equals('E'))
                    {
                        EndPoints.Add(new Tuple<int, int>(j, i));
                    }
                    map.Add(new Tuple<int,int>(j,i), col.ToString());
                    j++;
                }
                i++;
            }
            _mapDictionary = map;
        }

        public void SetElementAt(Tuple<int, int> coordinate,string elementValue)
        {
            _mapDictionary[coordinate] = elementValue;
        }

        public string GetElementAt(Tuple<int, int> coordinate)
        {
            if(_mapDictionary.TryGetValue(coordinate, out var mapElem))
            {
                    return mapElem;
            }
            throw new ArgumentException($"Coordinate ({coordinate.Item1},{coordinate.Item2}) not found");
        }

        public string Show()
        {
            var sb = new StringBuilder();
           for(var i = 0; i <= yMax; i++)
            {
                for(var j = 0; j <= xMax; j++)
                {
                    _mapDictionary.TryGetValue(new Tuple<int, int>(j, i), out var mapElem);
                    sb.Append(mapElem);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}