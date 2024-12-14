using AoC._2024;

namespace AoC.Tests._2024
{
    public class Day9Tests
    {

        //[Fact]
        //public void DayPart1Expand()
        //{
        //    string input = @"12345";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("0..111....22222");
        //}

        //[Fact]
        //public void DayPart2Expand()
        //{
        //    string input = @"12345";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("022111222");
        //}

        //[Fact]
        //public void DayPart2Expand2()
        //{
        //    string input = @"12365";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("022111222");
        //}

        //[Fact]
        //public void DayPart2Expand3()
        //{
        //    string input = @"12315";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("022111222");
        //}

        //[Fact]
        //public void DayPart2Expand4()
        //{
        //    string input = @"2333133121414131402";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("0099811188827773336446555566");
        //}

        //[Fact]
        //public void DayPart2Expand5()
        //{
        //    string input = @"414131402";

        //    var num = Day9.CompressForAmphipod(input);
        //    num.Should().Be("00004111142223333");
        //}


        [Fact]
        public void DayPart2Expand3()
        {
            string input = @"12345";

            var num = Day9.FindChecksumOfCompressedFileSystem(input);
            num.Should().Be(60);
        }

        [Fact]
        public void DayPart2Expand4()
        {
            string input = @"2333133121414131402";

            var num = Day9.FindChecksumOfCompressedFileSystem(input);
            num.Should().Be(1928);
        }
    }
}
