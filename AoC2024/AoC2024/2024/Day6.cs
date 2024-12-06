using static AoC._2024.Day6;

namespace AoC._2024
{
    public class Day6
    {
        public enum States
        {
            Open,
            Traversed,
            Obstacle,
            VillainSouth,
            VillainNorth,
            VillainEast,
            VillainWest,
            OutOfBounds
        }

        public enum Direction
        {
            North,
            East,
            South,
            West
        }

        public record struct Coordinate(int X, int Y);

        public static int GetDistinctPositionsOfGuardPath(string input)
        {
            var map = GetMap(input);
            var (startingPosition, startingDirection) = GetVillainStart(map);
            var traversedMap = TraverseMap(map, startingPosition, startingDirection);

            var countOfTraversedPositions = traversedMap
                .SelectMany(x => x)
                .Count(x => x == States.Traversed);

            return countOfTraversedPositions;

        }

        private static States[][] TraverseMap(States[][] map, Coordinate start, Direction direction)
        {
            Coordinate next = GetNextCoordinate(start, direction);
            Coordinate current = start;

            while (map.GetState(current) != States.OutOfBounds)
            {
                map.SetState(current, States.Traversed);
                current = next;
                next = GetNextCoordinate(current, direction);

                while (map.GetState(next) == States.Obstacle)
                {
                    direction = TurnRight(direction);
                    next = GetNextCoordinate(current, direction);
                }
            }

            return map;
        }

        private static Direction TurnRight(Direction direction)
        {
            // not as readable, but I think I used all my switch tokens for the day.
            return (Direction)((int)(direction + 1) % 4);
        }

        private static Coordinate GetNextCoordinate(Coordinate current, Direction direction)
        {
            return direction switch
            {
                Direction.North => new Coordinate(current.X, current.Y - 1),
                Direction.South => new Coordinate(current.X, current.Y + 1),
                Direction.East => new Coordinate(current.X + 1, current.Y),
                Direction.West => new Coordinate(current.X - 1, current.Y),
                _ => throw new ArgumentException("bad direction")
            }; 
        }

        private static States[][] GetMap(string input)
        {
            var stateCharMap = new Dictionary<char, States>()
            {
                ['.'] = States.Open,
                ['X'] = States.Traversed,
                ['#'] = States.Obstacle,
                ['v'] = States.VillainSouth,
                ['^'] = States.VillainNorth,
                ['>'] = States.VillainWest,
                ['<'] = States.VillainEast
            };

            return input
                .Split(Environment.NewLine)
                .Select(x => x.Select(c => stateCharMap[c]).ToArray())
                .ToArray();
        }

        private static (Coordinate startingPosition, Direction direction) GetVillainStart(States[][] grid)
        {
            var villainStates = new List<States>() { States.VillainSouth, States.VillainNorth, States.VillainEast, States.VillainWest };
            Coordinate startingPosition = new Coordinate(-1, -1);
            Direction direction = Direction.East;

            for (var i = 0; i < grid.Length; i++)
            {
                for (var j = 0; j < grid[i].Length; j++)
                {
                    if (villainStates.Contains(grid[i][j]))
                    {
                        //Typical coordinates x,y are along axis, 2D array the first coordinate is the Y axis therefore flipping here allows more natural indexing later.
                        startingPosition = new Coordinate(j, i);

                        //map state to direction
                        direction = grid[i][j] switch
                        {
                            States.VillainSouth => Direction.South,
                            States.VillainNorth => Direction.North,
                            States.VillainEast => Direction.East,
                            States.VillainWest => Direction.West,
                            _ => throw new ArgumentException("grid invalid")
                       };

                    }
                }
            }

            return (startingPosition, direction);
        }
    }

    public static class Extensions
    {
        //would be better to wrap 2d array in it's own type to prevent direct access and could provide coordinate indexer.
        public static States GetState(this States[][] map, Coordinate coordinate)
        {
            //TIL var pattern, but theres got to be some syntactical sugar I'm missing.
            //again coordinate axis are opposite of 2d array indexers - so they are flipped.
            return (coordinate.X, coordinate.Y) switch
            {
                var (x, y) when (x < 0 || y < 0 || y >= map.Length || x >= map[y].Length) => States.OutOfBounds,
                _ => map[coordinate.Y][coordinate.X]
            };
        }

        public static void SetState(this States[][] map, Coordinate coordinate, States state)
        {
            map[coordinate.Y][coordinate.X] = state;
        }
    }
}
