
using AoC._2024;

namespace AoC.Tests._2024;

public class Day7Tests
{
    [Fact]
    public void Day7Part1_Test()
    {
        string input = @"60: 2 5 2 3";

        Day7.SumWorkableEquations(input).Should().Be(60);
    }

    [Fact]
    public void Day7Part1_Test2()
    {
        string input = @"
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

        Day7.SumWorkableEquations(input).Should().Be(3749);
    }

    [Fact]
    public void Day7Part3_Test2()
    {
        string input = @"
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

        Day7.SumWorkableEquations(input, true).Should().Be(11387);
    }
}