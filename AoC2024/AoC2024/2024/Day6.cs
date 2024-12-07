using System.Diagnostics;
using System.Text;
using static AoC._2024.Day6;

namespace AoC._2024
{
    public class Day6
    {
        //What started as open, traversed, obstacle, villain. it not holds metadata - makes it easy, but not a great model. oh geez, now I'm adding flags.
        [Flags]
        public enum States
        {
            Open = 1 ,
            TraversedNorth = 2,
            TraversedEast = 4,
            TraversedSouth = 8,
            TraversedWest = 16,
            Obstacle = 32,
            VillainSouth = 64,
            VillainNorth = 128,
            VillainEast = 256,
            VillainWest = 512,
            OutOfBounds = 1024
        }

        [Flags]
        public enum Direction
        {
            North = 1,
            East = 2,
            South = 4,
            West = 8
        }

        public static int GetDistinctPositionsOfGuardPath(string input)
        {
            var map = Map.FromString(input);
            var (startingPosition, startingDirection) = map.GetVillainStart();
            var traversedMap = TraverseMap(map, startingPosition, startingDirection);

            var countOfTraversedPositions = traversedMap.NumberOfTraversed;

            return countOfTraversedPositions;
        }

        public static int GetNumberOfBlockablePaths(string input)
        {
            var map = Map.FromString(input);
            var (startingPosition, startingDirection) = map.GetVillainStart();
            var traversedMap = FindNumberOfBlockablePaths(map, startingPosition, startingDirection);

            return traversedMap;
        }

        private static Map TraverseMap(Map map, MapCoordinate start, Direction direction)
        {
            var next = start.Next(direction);
            var current = start;

            while (!current.IsOutOfBounds)
            {
                map.SetTraversed(current, direction);
                current = next;
                next = current.Next(direction);

                while (next.HasObstacle)
                {
                    direction = TurnRight(direction);
                    next = current.Next(direction);
                }
            }

            return map;
        }

        private static int FindNumberOfBlockablePaths(Map map, MapCoordinate start, Direction direction)
        {
            var next = start.Next(direction);
            var current = start;

            var numOfCycleInducingObstacles = 0;

            while (!current.IsOutOfBounds)
            {
                var doesPlacingObstacleCauseCycle = DoesPlacingObstacleCauseCycle(map, current, direction);

                if (doesPlacingObstacleCauseCycle && next != start)
                {
                    numOfCycleInducingObstacles++;
                }

                current.Traverse(direction);
                current = next;
                next = current.Next(direction);

                while (next.HasObstacle)
                {
                    direction = TurnRight(direction);
                    next = current.Next(direction);
                }
            }

            return numOfCycleInducingObstacles;
        }
        private static bool DoesPlacingObstacleCauseCycle(Map map, MapCoordinate currentCoordinate, Direction direction)
        {
            //clone existing map so that we can record traversal without altering the original map
            var mapForProbingTraversal = map.Clone();
            var probingCoordinate = currentCoordinate with { Map = mapForProbingTraversal };

            var nextCoordinate = probingCoordinate.Next(direction);

            // 1 we should only place an obstance on an open space
            // 2 by avoid placing where we already traversed then we shouldn't need to brute force and check
            // each obstacle position before and run full iterations.  Basically, if we've already traversed it then
            // this algorithm has already tried to place an obstacle there.  If we place it now then the obstacle didn't get
            // placed before the villain started moving.  
            if (!nextCoordinate.IsOpen)
                return false;

            mapForProbingTraversal.PlaceObstacle(nextCoordinate);

            var currentDirection = direction;

            //Traverse map from current position to see if we land on a position that's already traversed
            //In the same direction.  Work with tempmap as to not alter the original when probing for cycle.
            while (!probingCoordinate.IsOutOfBounds)
            {
                if (probingCoordinate.HasBeenTraversed(currentDirection))
                    return true;

                nextCoordinate = probingCoordinate.Next(currentDirection);
                var nextDirection = currentDirection;

                while (nextCoordinate.HasObstacle)
                {
                    nextDirection = TurnRight(nextDirection);
                    nextCoordinate = probingCoordinate.Next(nextDirection);
                }

                //Set traversals with turns, makes printing, and cycle detection easier.
                //Otherwise, do you count the direction your were coming or going as the path?
                mapForProbingTraversal.SetTraversed(probingCoordinate, currentDirection | nextDirection);

                currentDirection = nextDirection;
                probingCoordinate = nextCoordinate;
            }

            return false;
        }

        private static Direction TurnRight(Direction direction)
        {
            return direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentException("bad direction")
            };
        }
    }

    public static class Extensions
    {
        public static bool IsTraversal(this States state)
        {
            States traversedStates = States.TraversedNorth | States.TraversedEast | States.TraversedSouth | States.TraversedWest;

            return ((state & traversedStates) != 0);
        }
    }

    public class Map
    {
        private readonly States[][] _map;

        public Map(States[][] map)
        {
            _map = map;
        }

        public int NumberOfTraversed => _map
            .SelectMany(x => x)
            .Count(x => x.IsTraversal());

        public static Map FromString(string input)
        {
            var stateCharMap = new Dictionary<char, States>()
            {
                ['.'] = States.Open,
                ['#'] = States.Obstacle,
                ['v'] = States.VillainSouth,
                ['^'] = States.VillainNorth,
                ['>'] = States.VillainWest,
                ['<'] = States.VillainEast
            };

            var mapArray = input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Select(c => stateCharMap[c]).ToArray())
                .ToArray();

            return new Map(mapArray);
        }

        //would be better to wrap 2d array in it's own type to prevent direct access and could provide coordinate indexer.
        public States GetState(MapCoordinate coordinate)
        {
            //TIL var pattern, but theres got to be some syntactical sugar I'm missing.
            //again coordinate axis are opposite of 2d array indexers - so they are flipped.
            return (coordinate) switch
            {
                _ when IsOutOfBounds(coordinate) => States.OutOfBounds,
                _ => _map[coordinate.Y][coordinate.X]
            };
        }


        public bool IsOutOfBounds(MapCoordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.Y < 0 || coordinate.Y >= _map.Length || coordinate.X >= _map[coordinate.Y].Length;
        }

        public void PlaceObstacle(MapCoordinate coordinate)
        {
            if (!coordinate.IsOutOfBounds)
            {
                _map[coordinate.Y][coordinate.X] = States.Obstacle;
            }
        }

        public void SetTraversed(MapCoordinate coordinate, Direction direction)
        {
            if (_map[coordinate.Y][coordinate.X].IsTraversal())
            {
                _map[coordinate.Y][coordinate.X] |= DirectionToTraversalState(direction);
            }
            else
            {
                _map[coordinate.Y][coordinate.X] = DirectionToTraversalState(direction);
            }
        }

        public bool HasBeenTraversed(MapCoordinate coordinate, Direction direction)
        {
            var traversalType = DirectionToTraversalState(direction);
            if ((_map[coordinate.Y][coordinate.X] & traversalType) == traversalType)
            {
                return true;
                //map.Print();  //print probed traversal on found cycles for debugging
            }
            return false;
        }

        private static States DirectionToTraversalState(Direction direction)
        {
            States result = 0;

            //handles combining turns so if we hit an obstance heading west the current position would be noted
            //as traversed with States.West | States.South
            result |= (direction & Direction.North) != 0 ? States.TraversedNorth : 0;
            result |= (direction & Direction.East) != 0 ? States.TraversedEast : 0;
            result |= (direction & Direction.South) != 0 ? States.TraversedSouth : 0;
            result |= (direction & Direction.West) != 0 ? States.TraversedWest : 0;

            return result;
        }

        /// <summary>
        /// Get Starting position of Villain w/ Direction
        /// </summary>
        /// <returns></returns>
        /// <remarks>Assumes all maps have a starting villain</remarks>
        /// <exception cref="ArgumentException"></exception>
        public (MapCoordinate startingPosition, Direction direction) GetVillainStart()
        {
            var villainStates = new List<States>() { States.VillainSouth, States.VillainNorth, States.VillainEast, States.VillainWest };
            Direction direction = Direction.East;
            MapCoordinate startingPosition = null;

            for (var i = 0; i < _map.Length; i++)
            {
                for (var j = 0; j < _map[i].Length; j++)
                {
                    if (villainStates.Contains(_map[i][j]))
                    {
                        //Typical coordinates x,y are along axis, 2D array the first coordinate is the Y axis therefore flipping here allows more natural indexing later.
                        startingPosition = new MapCoordinate(this, j, i);

                        //map state to direction
                        direction = GetState(startingPosition) switch
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

        public void Print()
        {
            Debug.WriteLine("");
            var line = new StringBuilder();

            for (var i = 0; i < _map.Length; i++)
            {
                for (var j = 0; j < _map[0].Length; j++)
                {
                    var character = _map[i][j] switch
                    {
                        States.Open => '.',
                        States.Obstacle => '#',
                        States.VillainSouth => 'v',
                        States.VillainNorth => '^',
                        States.VillainEast => '>',
                        States.VillainWest => '<',
                        var state when (state & (States.TraversedNorth | States.TraversedSouth)) == (States.TraversedNorth | States.TraversedSouth) => '║', // double traversed vertical line
                        var state when (state & (States.TraversedEast | States.TraversedWest)) == (States.TraversedEast | States.TraversedWest) => '═', // double traversed horizontal line
                        var state when (state & (States.TraversedSouth | States.TraversedEast)) == (States.TraversedSouth | States.TraversedEast) => '┐', // top-left corner
                        var state when (state & (States.TraversedSouth | States.TraversedWest)) == (States.TraversedSouth | States.TraversedWest) => '┘', // Bottom-right corner
                        var state when (state & (States.TraversedNorth | States.TraversedEast)) == (States.TraversedNorth | States.TraversedEast) => '┌', // Top-left corner
                        var state when (state & (States.TraversedNorth | States.TraversedWest)) == (States.TraversedNorth | States.TraversedWest) => '└',// Boom-left corner
                        States.TraversedEast => '-',
                        States.TraversedSouth => '|',
                        States.TraversedWest => '_',
                        States.TraversedNorth => '|',
                        _ => '?'
                    };

                    line.Append(character);
                }
                Debug.WriteLine(line.ToString());
                line.Clear();
            }
            Debug.WriteLine("");
        }

        public Map Clone()
        {
            return new Map(_map.Select(x => x.Select(y => y).ToArray()).ToArray());
        }
    }

    public record MapCoordinate(Map Map, int X, int Y)
    {
        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public bool IsOpen => Map.GetState(this) == States.Open;
        
        public bool IsOutOfBounds => Map.IsOutOfBounds(this);
        public bool HasObstacle => Map.GetState(this) == States.Obstacle;

        public void Traverse(Direction direction)
        {
            Map.SetTraversed(this, direction);
        }

        public bool HasBeenTraversed(Direction direction)
        {
            return Map.HasBeenTraversed(this, direction);
        }

        public MapCoordinate Next(Direction direction)
        {
            return direction switch
            {
                Direction.North => this with { Y = this.Y - 1 },
                Direction.South => this with { Y = this.Y + 1 },
                Direction.East => this with { X = this.X + 1 },
                Direction.West => this with { X = this.X - 1 },
                _ => throw new ArgumentException("bad direction")
            };
        }
    }
}
