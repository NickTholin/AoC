namespace AoC._2024
{
    public static class Day5
    {
        public static int FindSumOfMiddleOfPrintedPageNumbers(string input)
        {
            var inputDelimitedByLine = input.Split(Environment.NewLine).ToList();
            var indexOfInstructionDelimited = inputDelimitedByLine.IndexOf(string.Empty);
            var pageOrderingInput = inputDelimitedByLine.Take(indexOfInstructionDelimited);
            var printingInstructions = inputDelimitedByLine.Skip(indexOfInstructionDelimited + 1)
                .Select(x => x.Split(",")
                    .Select(int.Parse).ToList())
                .ToList();

            var printingInstructionLookup = GetPrintingInstructionLookup(pageOrderingInput);

            var pagesToPrint = GetPagesToPrint(printingInstructionLookup, printingInstructions);

            return pagesToPrint.Sum(x => x[x.Count / 2]);
        }

        public static int FindSumOfMiddleOfFixedPrintedPageNumbers(string input)
        {
            var inputDelimitedByLine = input.Split(Environment.NewLine).ToList();
            var indexOfInstructionDelimited = inputDelimitedByLine.IndexOf(string.Empty);
            var pageOrderingInput = inputDelimitedByLine.Take(indexOfInstructionDelimited);
            var printingInstructions = inputDelimitedByLine.Skip(indexOfInstructionDelimited + 1)
                .Select(x => x.Split(",")
                    .Select(int.Parse).ToList())
                .ToList();

            var printingInstructionLookup = GetPrintingInstructionLookup(pageOrderingInput);

            var fixedPagesToPrint = FixedPagesToPrint(printingInstructionLookup, printingInstructions);

            return fixedPagesToPrint.Sum(x => x[x.Count / 2]);
        }

        public static Dictionary<int, List<int>> GetPrintingInstructionLookup(IEnumerable<string> printingInstructions)
        {
            var printingInstructionLookup = new Dictionary<int, List<int>>();

            foreach(var printingInstruction in printingInstructions)
            {
                var printingInstructionParts = printingInstruction.Split('|');
                var page = int.Parse(printingInstructionParts[0]);
                var pageBefore = int.Parse(printingInstructionParts[1]);

                if (printingInstructionLookup.ContainsKey(page))
                    printingInstructionLookup[page].Add(pageBefore);
                else
                    printingInstructionLookup[page] = new List<int>() { pageBefore };
           }

            return printingInstructionLookup;
        }

        public static List<List<int>> GetPagesToPrint(Dictionary<int, List<int>> printingInstructionLookup, List<List<int>> printingInstructions)
        {
            var validPagesToPrint = new List<List<int>>();
            var comparer = new PagePriority(printingInstructionLookup);

            foreach (var pagesToPrint in printingInstructions)
            {
                var sortedPagesToPrint = pagesToPrint.OrderBy(x => x, comparer).ToList();

                if (pagesToPrint.SequenceEqual(sortedPagesToPrint))
                    validPagesToPrint.Add(sortedPagesToPrint);
            }

            return validPagesToPrint;
        }

 
        public static List<List<int>> FixedPagesToPrint(Dictionary<int, List<int>> printingInstructionLookup, List<List<int>> printingInstructions)
        {

            var correctPrintInstructions = new List<List<int>>();
            var comparer = new PagePriority(printingInstructionLookup);

            foreach (var pagesToPrint in printingInstructions)
            {
                var sortedPagesToPrint = pagesToPrint.OrderBy(x => x, comparer).ToList();

                if (!pagesToPrint.SequenceEqual(sortedPagesToPrint))
                    correctPrintInstructions.Add(sortedPagesToPrint);
            }

            return correctPrintInstructions;
        }

        public class PagePriority : IComparer<int>
        {
            private readonly Dictionary<int, List<int>> _pagePriorityLookup;

            public PagePriority(Dictionary<int, List<int>> pagePriorityLookup)
            {
                _pagePriorityLookup = pagePriorityLookup;
            }

            public int Compare(int x, int y)
            {
                if (_pagePriorityLookup.ContainsKey(x) && _pagePriorityLookup[x].Contains(y))
                    return -1;
                else
                    return 1;
            }
        }
    }
}
