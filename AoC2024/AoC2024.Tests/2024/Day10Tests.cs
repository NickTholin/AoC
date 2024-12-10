using AoC._2024;

namespace AoC.Tests._2024;
public class Day10Tests
{
    [Fact]
    public void Day10Part1_Test()
    {
        string input = @"
0123
1234
8765
9876";

        var num = Day10.FindTrailHeads(input);
        num.Should().Be(1);
    }

    [Fact]
    public void Day10Part1_Test2()
    {
        string input = @"
9990999
9991999
9992999
6543456
7999997
8111118
9111119";

        var num = Day10.FindTrailHeads(input);
        num.Should().Be(2);
    }

    [Fact]
    public void Day10Part1_Test4()
    {
        string input = @"
1011911
2111811
3111711
4567654
1118113
1119112
1111101";

        var num = Day10.FindTrailHeads(input);
        num.Should().Be(3);
    }

    [Fact]
    public void Day10Part1_Test5()
    {
        string input = @"
7077707
2177212
3777353
4777454
5777565
6111171
7111181
8111191
9111111";

//.0...0.
//21..212
//3...3.3
//4...454
//5...565
//6....7.
//7....8.
//8....9.
//9......";
        var num = Day10.FindTrailHeads(input);
        num.Should().Be(2);
    }

    [Fact]
    public void Day10Part1_Test3()
    {
        string input = @"
89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732";

        var num = Day10.FindTrailHeads(input);
        num.Should().Be(36);
    }

}
