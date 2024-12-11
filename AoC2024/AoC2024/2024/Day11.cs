using AoC.Common;

namespace AoC._2024
{
    public class Day11 
    {
        public static long TotalNumberOfStonesAfterBlinking(string input, int blinkTimes)
        {
            var stones = input.ToIEnumerable<long>(long.Parse, ' ');
            var result = Blink(stones, blinkTimes);

            return result;
        }

        private static long Blink(IEnumerable<long> stones, int times)
        {
            var numStones = 0L;

            var knownBlinkValues = new Dictionary<(long, int), long>();

            foreach (var stone in stones)
            {
                numStones += BlinkManyTimes(stone, times, knownBlinkValues);
            }

            return numStones;
        }

        private static long BlinkManyTimes(long engraving, int timesToBlink, Dictionary<(long,  int), long> knownBlinkEngravingResults)
        {
            var countOfStones = 0L;
            if (timesToBlink == 0) return 1;

            if (knownBlinkEngravingResults.ContainsKey((engraving, timesToBlink)))
            {
                return knownBlinkEngravingResults[(engraving, timesToBlink)];
            }

            var HasEvenDigits = (long x) => x.ToString().Length % 2 == 0;

            long stones;

            switch (engraving)
            {
                case 0:
                    stones = BlinkManyTimes(1L, timesToBlink - 1, knownBlinkEngravingResults);
                    break;

                case var _ when HasEvenDigits(engraving):
                    var (first, second) = SplitStone(engraving);
                    stones = BlinkManyTimes(first, timesToBlink - 1, knownBlinkEngravingResults);
                    stones += BlinkManyTimes(second, timesToBlink - 1, knownBlinkEngravingResults);
                    break;

                default:
                    stones = BlinkManyTimes(engraving * 2024, timesToBlink - 1, knownBlinkEngravingResults);
                    break;
            }

            countOfStones += stones;
            knownBlinkEngravingResults.Add((engraving, timesToBlink), stones);

            return countOfStones;
        }

        private static (long first, long second) SplitStone(long stone)
        {
            var stoneAsString = stone.ToString();

            var stone1 = long.Parse(stoneAsString.AsSpan().Slice(0, stoneAsString.Length / 2));
            var stone2 = long.Parse(stoneAsString.AsSpan().Slice(stoneAsString.Length / 2));

            return (stone1, stone2);
        }
    }

}
