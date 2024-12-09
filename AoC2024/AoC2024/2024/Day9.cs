
using System.Collections.Generic;
using System.Text;

namespace AoC._2024;

public class Day9
{
    public static string CompressForAmphipod(string input)
    {
        var disk = CreateDisk(input);
        return ExpandDisk(disk);
    }

    public static List<DiskSegment> Compress(List<DiskSegment> source)
    {
        var compressed = new List<DiskSegment>();
        int freeSpace = source.Sum(x => x.IsFreeSpace ? x.Length : 0);

        var reversedSource = new Stack<DiskSegment>(compressed.AsEnumerable().Reverse());


        int i = 0;

        do
        {
            var segment = source[i];
            if (segment.IsFile)
            {
                compressed.Add(segment);
            }
            else
            {
                var freeSpaceLength = segment.Length;

                while (freeSpaceLength > 0)
                {
                    var segmentFromBack = reversedSource.Peek();
                    if (segmentFromBack.IsFile)
                    {
                        //Only pop if we have enough space to fill.  That way we can split files from the back across multiple openings
                        if (segmentFromBack.Length >= freeSpaceLength)
                        {

                        }
                    }
                    else
                    {
                        reversedSource.Pop(); // discard freespace
                    }
                }
            }


        } while (freeSpace > 0);

        return compressed;
    }



    // For troubleshooting
    public static string ExpandDisk(List<DiskSegment> disk)
    {
        var expandedDisk = new StringBuilder();
        //does not print nicely if ID > 9
        foreach (var segment in disk)
        {
            expandedDisk.Append(
                segment.IsFile
                ? String.Concat(Enumerable.Repeat(segment.Id, segment.Length)) 
                : new string('.', segment.Length));
        }
        return expandedDisk.ToString();
    }

    public static List<DiskSegment> CreateDisk(string compressedFile)
    {
        var disk = new List<DiskSegment>();

        var isFile = true;
        var fileId = 0;

        foreach (var length in compressedFile.Select(x => int.Parse(x.ToString())))
        {
            if (isFile)
            {
                disk.Add(new DiskSegment(fileId, length));
                fileId++;
            }
            else
                disk.Add(DiskSegment.FreeSpace(length));

            isFile = !isFile;
        }

        return disk;
    }

    public readonly struct DiskSegment(int Id, int Length)
    {
        public int Id { get; } = Id; 
        public int Length { get; } = Length; 
        public bool IsFreeSpace { get; init; } = false;
        public bool IsFile => !IsFreeSpace;

        public static DiskSegment FreeSpace(int length) => new(0, length) { IsFreeSpace = true };
    }
}