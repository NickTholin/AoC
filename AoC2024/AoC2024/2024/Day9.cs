using System.Text;

namespace AoC._2024;

public class Day9
{
    public static long FindChecksumOfCompressedFileSystem(string input)
    {
        var disk = CreateDisk(input);
        var compressed = Compress(disk);
        var expanded = ExpandDisk(compressed);
        //return compressed.Aggregate((int acc, DiskSegment segment) => acc + segment.Id * segment.);
        return expanded
            .Select((segment, index) => new { segment, index })
            .Aggregate(0L, (sum, item) => sum + (item.segment.Id * item.index));
    }

    public static List<DiskSegment> Compress(List<DiskSegment> source)
    {
        var compressed = new List<DiskSegment>();
        var totalFreeSpace = source.Sum(x => x.IsFreeSpace ? x.Length : 0);
        var current = 0;
        var ranOutOfFiles = false;
        var lastBlock = source.Count - 1;

        do
        {
            var segment = source[current];

            if (segment.IsFile)
            {
                compressed.Add(segment);
                current++;
            }
            else if (segment.Length == 0)
            {
                current++;
            }
            else
            {
                var freeSpaceLength = segment.Length;

                while (freeSpaceLength > 0)
                {
                    var segmentFromBack = source[lastBlock];

                    if (segmentFromBack.IsFreeSpace)
                    {
                        totalFreeSpace -= segmentFromBack.Length;
                        lastBlock--;
                        continue;
                    }

                    // if the source file is the one we are trying to place then there are available open spaces but no more files to condense.
                    if (current >= lastBlock)
                    {
                        ranOutOfFiles = true;
                        break;
                    }

                    if (segmentFromBack.IsFile)
                    {
                        //If the file is too big to fit in the free space, split it
                        if (segmentFromBack.Length > freeSpaceLength)
                        {
                            compressed.Add(new DiskSegment(segmentFromBack.Id, freeSpaceLength));

                            segmentFromBack.Length -= freeSpaceLength;
                            totalFreeSpace -= freeSpaceLength;
                            freeSpaceLength = 0;
                            source.RemoveAt(current);
                            lastBlock--;
                        }
                        else
                        {
                            //Only pop if we have enough space to fill.  That way we can split files from the back across multiple openings
                            compressed.Add(new DiskSegment(segmentFromBack.Id, segmentFromBack.Length));
                            freeSpaceLength -= segmentFromBack.Length;
                            lastBlock--;

                            if (freeSpaceLength == 0)
                                current++; // move onto next file segment in source

                            totalFreeSpace -= segmentFromBack.Length;
                        }
                    }
                    else
                    {
                        lastBlock--;
                        totalFreeSpace -= segmentFromBack.Length;
                    }

                    //flush last file segment by appending it to the end if there are no more open segments.
                    if (totalFreeSpace == 0 && segmentFromBack.Length > 0)
                        compressed.Add(new DiskSegment(segmentFromBack.Id, segmentFromBack.Length));
                }
            }

        } while (totalFreeSpace > 0 && !ranOutOfFiles);

        return compressed;
    }

    public static List<DiskSegment> CompressDefrag(List<DiskSegment> source)
    {
        var compressed = new List<DiskSegment>();
        var totalFreeSpace = source.Sum(x => x.IsFreeSpace ? x.Length : 0);
        var current = 0;
        var ranOutOfFiles = false;
        var lastBlock = source.Count - 1;

        do
        {
            var segment = source[current];

            if (segment.IsFile)
            {
                compressed.Add(segment);
                current++;
            }
            else if (segment.Length == 0)
            {
                current++;
            }
            else
            {
                var freeSpaceLength = segment.Length;

                while (freeSpaceLength > 0)
                {
                    var segmentFromBack = source[lastBlock];

                    if (segmentFromBack.IsFreeSpace)
                    {
                        totalFreeSpace -= segmentFromBack.Length;
                        lastBlock--;
                        continue;
                    }

                    // if the source file is the one we are trying to place then there are available open spaces but no more files to condense.
                    if (current >= lastBlock)
                    {
                        ranOutOfFiles = true;
                        break;
                    }

                    if (segmentFromBack.IsFile)
                    {
                        //If the file is too big to fit in the free space, split it
                        if (segmentFromBack.Length > freeSpaceLength)
                        {
                            compressed.Add(new DiskSegment(segmentFromBack.Id, freeSpaceLength));

                            segmentFromBack.Length -= freeSpaceLength;
                            totalFreeSpace -= freeSpaceLength;
                            freeSpaceLength = 0;
                            source.RemoveAt(current);
                            lastBlock--;
                        }
                        else
                        {
                            //Only pop if we have enough space to fill.  That way we can split files from the back across multiple openings
                            compressed.Add(new DiskSegment(segmentFromBack.Id, segmentFromBack.Length));
                            freeSpaceLength -= segmentFromBack.Length;
                            lastBlock--;

                            if (freeSpaceLength == 0)
                                current++; // move onto next file segment in source

                            totalFreeSpace -= segmentFromBack.Length;
                        }
                    }
                    else
                    {
                        lastBlock--;
                        totalFreeSpace -= segmentFromBack.Length;
                    }

                    //flush last file segment by appending it to the end if there are no more open segments.
                    if (totalFreeSpace == 0 && segmentFromBack.Length > 0)
                        compressed.Add(new DiskSegment(segmentFromBack.Id, segmentFromBack.Length));
                }
            }

        } while (totalFreeSpace > 0 && !ranOutOfFiles);

        return compressed;
    }

    public static string ExpandDiskToString(List<DiskSegment> disk)
    {
        var expandedDisk = new StringBuilder();
        //does not print nicely if ID > 9
        foreach (var segment in disk)
        {
            expandedDisk.Append(
                segment.IsFile
                ? string.Concat(Enumerable.Repeat(segment.Id, segment.Length)) 
                : new string('.', segment.Length));
        }
        return expandedDisk.ToString();
    }

    // For troubleshooting
    public static List<DiskSegment> ExpandDisk(List<DiskSegment> disk)
    {
        var expandedDisk = new List<DiskSegment>();

        foreach (var diskSegment in disk)
        {
            expandedDisk.AddRange(Enumerable.Repeat(new DiskSegment(diskSegment.Id, diskSegment.Length), diskSegment.Length));
        }

        return expandedDisk;
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

    public class DiskSegment(int Id, int Length)
    {
        public int Id { get; } = Id; 
        public int Length { get; set; } = Length; 
        public bool IsFreeSpace { get; init; } = false;
        public bool IsFile => !IsFreeSpace;

        public static DiskSegment FreeSpace(int length) => new(0, length) { IsFreeSpace = true };
    }
}