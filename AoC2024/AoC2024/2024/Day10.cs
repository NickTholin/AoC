using AoC.Common;

namespace AoC._2024;

public static class Day10
{
    public static int FindTrailHeads(string input, bool onlyCountDistinctTrails = true)
    {
        var map = input.To2DArray(int.Parse);
        var trailStartCoordinates = map.FindCoordinates(0);
        var allFoundTrailEndCoordinates = new List<(int x, int y)>();
        var foundNewTrailEnds = new List<(int x, int y)>();
        foreach (var startCoordinate in trailStartCoordinates)
        {
            FindIncreasingPath(map, startCoordinate, foundNewTrailEnds);

            if (onlyCountDistinctTrails)
                foundNewTrailEnds = foundNewTrailEnds.Distinct().ToList();

            allFoundTrailEndCoordinates.AddRange(foundNewTrailEnds);
            foundNewTrailEnds.Clear();
        }

        return allFoundTrailEndCoordinates.Count();
    }

    public static void FindIncreasingPath(int[][] map, (int x, int y) coordinate, List<(int x, int y)> foundTrailEnds)
    {
        var thisValue = map[coordinate.y][coordinate.x];
        var target = thisValue + 1;

        if (thisValue == 9)
        {
            foundTrailEnds.Add(coordinate);
            return;
        }

        var increasingCoordinates = map.FindCoordinatesOfMatchingValueAdjacentToCoordinate(coordinate, target);

        foreach (var increasingCoordinate in increasingCoordinates)
        {
            FindIncreasingPath(map, increasingCoordinate, foundTrailEnds);
        }
    }


}

