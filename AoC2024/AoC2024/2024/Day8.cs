using System.Diagnostics;
using System.Text;

namespace AoC._2024
{
    public class Day8
    {
        //overwrote part 1 :(
        public static int NumberOfAntinodes(string input)
        {
            var map = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.ToCharArray())
                .ToArray();
            
            Dictionary<char, List<(int x, int y)>> antennas = new Dictionary<char, List<(int x, int y)>>();

            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    var coordinate = map[i][j];
                    if (char.IsAsciiLetterOrDigit(coordinate))
                    {
                        if (antennas.ContainsKey(coordinate))
                        {
                            antennas[coordinate].Add((i, j));
                        }
                        else
                        {
                            antennas[coordinate] = new List<(int x, int y)> { (i, j) };
                        }
                    }
                }
            }

            List<(int x, int y)> antenodes = new List<(int x, int y)>();

            foreach (var antennaPairs in antennas.Values)
            {
                antenodes.AddRange(FindAllDeltas(map, antennaPairs));
            }

            PlaceAntenodes(map, antenodes);
            Print(map);
            return map.SelectMany(x => x).Count(x => x == '#') + antennas.Sum(x=> (x.Value.Count > 1) ? x.Value.Count : 0);
        }

        private static void Print(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                var line = new StringBuilder();

                for (int j = 0; j < map[i].Length; j++)
                {
                    line.Append(map[i][j]);
                }

                Debug.WriteLine(line);
            }

            Debug.WriteLine("");
        }

        private static void PlaceAntenodes(char[][] map, List<(int x, int y)> antenodes)
        {
            foreach (var antenodeCoordinate in antenodes.Distinct())
            {
                var (x, y) = antenodeCoordinate;

                // place antenodeCoordinate in map without going out of bounds
                if (x >= 0 && y >= 0 && y < map.Length && x < map[y].Length)
                {
                    if (map[x][y] == '.')
                        map[x][y] = '#';
                }
            }
        }

        private static List<(int x, int y)> FindAllDeltas(char[][] map, List<(int x, int y)> coordinates)
        {
            var antenodes = new List<(int x, int y)>();

            foreach (var coordinate in coordinates)
            {
                List<(int rise, int run)> vectors = new List<(int rise, int run)>();

                foreach (var otherCoordinate in coordinates.Where(x => !x.Equals(coordinate)))
                {
                    (int rise, int run) = ((otherCoordinate.y - coordinate.y), (otherCoordinate.x - coordinate.x));

                    var temp = (coordinate.x, coordinate.y);
                    //calculate coordinates following vector going up 
                    while (true)
                    {
                        var first = (temp.x - run, temp.y - rise);
                        var firstInBounds = IsInBounds(map, first);

                        if (!firstInBounds) break;
                        antenodes.Add(first);

                        temp = first;
                    }

                    //calculate coordinates following vector going down
                    temp = (coordinate.x, coordinate.y);
                    while (true)
                    {
                        var second = (temp.x + run, temp.y + rise);
                        var secondsInBounds = IsInBounds(map, second);
                        if (!secondsInBounds) break;
                        antenodes.Add(second);
                        temp = second;
                    }
                }
            }

            return antenodes;
        }

        private static bool IsInBounds(char[][] map, (int x, int y) coordinate)
        {
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.y < map.Length && coordinate.x < map[coordinate.y].Length;
        }

    }
}
