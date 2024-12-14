using AoC.Common;

namespace AoC._2024;

public class Day12
{
    public static int FindCostOfGardenFence(string input)
    {
        var fenceMap = input.To2DArray(char.Parse);
        var plots = GetPlots(fenceMap);
        //Part1
        //var areaAndPerimeters = CalculateAreaAndPerimeter(plots);
        var areaAndSides = CalculateAreaAndSides(plots);

        var totalFenceCost = areaAndSides
            .Aggregate(0, (sum, next) => sum += (next.area * next.sides));

        return totalFenceCost;
    }

    //Part1
    private static List<(int area, int perimeter)> CalculateAreaAndPerimeter(Dictionary<string, IEnumerable<MapCoordinate>> plots)
    {
        const int MAX_PERIMETER = 4;
        var areaAndPerimeters = new List<(int, int)>();

        foreach (var (plot, coordinates) in plots)
        {
            var groupPlotPerimeter = 0;

            foreach (var coordinate in coordinates)
            {
                var numberOfAdjacentCoordinates = 0;

                foreach(var otherCoordinate in coordinates)
                {
                    if (coordinate.IsAdjacentTo(otherCoordinate))
                        numberOfAdjacentCoordinates++;
                }

                groupPlotPerimeter += MAX_PERIMETER - numberOfAdjacentCoordinates; 
            }

            areaAndPerimeters.Add((coordinates.Count(),  groupPlotPerimeter));
        }

        return areaAndPerimeters;
    }

    //Part2
    private static List<(int area, int sides)> CalculateAreaAndSides(Dictionary<string, IEnumerable<MapCoordinate>> plots)
    {
        var areaAndSides = new List<(int, int)>();

        foreach (var (_, coordinates) in plots)
        {
            var corners = coordinates.Sum(coordinate => CountCorners(coordinate, coordinates));
            areaAndSides.Add((coordinates.Count(), corners));
        }

        return areaAndSides;
    }

    public static int CountCorners(MapCoordinate coordinate, IEnumerable<MapCoordinate> groupCoordinates)
    {
        var corners = 0;
        //Place diagonals to current coordinate last so we do not have to pass metadata
        (int x, int y)[] coordinatesTouchingTopLeftCorner =
        [
            (coordinate.X, coordinate.Y - 1),
            (coordinate.X - 1, coordinate.Y),
            (coordinate.X - 1, coordinate.Y - 1),
        ];

        (int x, int y)[] coordinatesTouchingTopRightCorner =
        [
            (coordinate.X, coordinate.Y - 1),
            (coordinate.X + 1, coordinate.Y),
            (coordinate.X + 1, coordinate.Y - 1),

        ];

        (int x, int y)[] coordinatesTouchingBottomRightCorner =
        [
            (coordinate.X + 1, coordinate.Y),
            (coordinate.X, coordinate.Y + 1),
            (coordinate.X + 1, coordinate.Y + 1),
        ];

        (int x, int y)[] coordinatesTouchingBottomLeftCorner =
        [
            (coordinate.X - 1, coordinate.Y),
            (coordinate.X, coordinate.Y + 1),
            (coordinate.X - 1, coordinate.Y + 1),
        ];

        (int x, int y)[][] cornerCoordinateGroups = [
            coordinatesTouchingTopLeftCorner,
            coordinatesTouchingTopRightCorner,
            coordinatesTouchingBottomRightCorner,
            coordinatesTouchingBottomLeftCorner];

        for (int i = 0; i < 4; i++)
        {
            var touchingCoordinates = cornerCoordinateGroups[i];

            var hasTopOrBottomInGroup = groupCoordinates.Any(gc => gc.X == touchingCoordinates[0].x && gc.Y == touchingCoordinates[0].y);
            var hasSideInGroup = groupCoordinates.Any(gc => gc.X == touchingCoordinates[1].x && gc.Y == touchingCoordinates[1].y);
            var hasDiagonalInGroup = groupCoordinates.Any(gc => gc.X == touchingCoordinates[2].x && gc.Y == touchingCoordinates[2].y);

            var missingTopAndSide = !hasTopOrBottomInGroup && !hasSideInGroup;
            // Missing top and side therefore outside corner.
            if (missingTopAndSide)
                corners++;
            // Missing only diagonal therefore inside corner.
            else if (!hasDiagonalInGroup && hasTopOrBottomInGroup && hasSideInGroup)
                corners++;
        }

        return corners;
    }

    public static Dictionary<string, IEnumerable<MapCoordinate>> GetPlots(char[][] map)
    {
        var groupedPlots = new Dictionary<string, IEnumerable<MapCoordinate>>();
        var handledPlots = new List<MapCoordinate>();

        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                if (handledPlots.Contains(new MapCoordinate(x, y))) continue;

                var value = map[y][x];
                var groupCoordinates = new List<MapCoordinate>();

                GetAdjacentCoordinates(map, new MapCoordinate(x, y), value, groupCoordinates);

                //num group allows us to have multiple groups with the same plot value incase plant forms a separate group
                //Only reason not to return just a nest IEnumerable is to provide the plant type for debugging
                var numGroup = 0;
                while (groupedPlots.ContainsKey($"{value}{numGroup}"))
                {
                    numGroup++;
                }

                groupedPlots.Add($"{value}{numGroup}", groupCoordinates);
                handledPlots.AddRange(groupCoordinates);
            }
        }

        return groupedPlots;
    }

    public static void GetAdjacentCoordinates(char[][] map, MapCoordinate coordinate, char value, List<MapCoordinate> groupCoordinates)
    {
        var adjacentCoordinates = map.FindCoordinatesOfMatchingValueAdjacentToCoordinate((coordinate.X, coordinate.Y), value, false);
        groupCoordinates.Add(coordinate);
        foreach (var adjacentCoordinate in adjacentCoordinates.Where(x=> !groupCoordinates.Contains(new MapCoordinate(x.x, x.y))))
        {
            GetAdjacentCoordinates(map, new MapCoordinate(adjacentCoordinate.x, adjacentCoordinate.y), value, groupCoordinates);
        }
    }
}
