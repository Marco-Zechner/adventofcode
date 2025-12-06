namespace MarcoZechner.AdventOfCode._2025.Day5
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "data.txt";
            if (args.Length > 0)
                fileName = args[0];
            var (ranges, numbers) = ParseInput(fileName);
            var mergedRanges = SortAndMergeRanges(ranges);

            var countInRanges = numbers.Count(number => mergedRanges.Any(r => number >= r.Start && number <= r.End));

            Console.WriteLine("Number of Fresh Ingredients:\n" + countInRanges);
            var totalCovered = mergedRanges.Sum(r => (long)(r.End - r.Start + 1));
            Console.WriteLine("Total Number of Fresh Ranges:\n" + totalCovered);
        }

        private static List<(ulong Start, ulong End)> SortAndMergeRanges(List<(ulong Start, ulong End)> unsortedRange)
        {
            var sortedByStart = unsortedRange.OrderBy(r => r.Start).ToList();
            var mergedRanges = new List<(ulong Start, ulong End)>();

            foreach (var range in sortedByStart)
            {
                if (mergedRanges.Count == 0 || mergedRanges.Last().End < range.Start)
                    mergedRanges.Add(range); // start new range
                else
                {
                    var lastRange = mergedRanges.Last();
                    mergedRanges[^1] = (lastRange.Start, Math.Max(lastRange.End, range.End)); // extend last range
                }
            }

            return mergedRanges;
        }
        
        private static (List<(ulong Start, ulong End)> ranges, List<ulong> numbers) ParseInput(string filename)
        {
            var path = Path.Combine(AppContext.BaseDirectory, "data", filename);
            var input = File.ReadAllText(path);
            input = input.Replace("\r", ""); // Normalize line endings for Windows

            var ranges = input.Split("\n\n")[0]
                .Split('\n')
                .Select(line => line.Split('-'))
                .Select(parts => (Start: ulong.Parse(parts[0]), End: ulong.Parse(parts[1])))
                .ToList();
    
            var numbers = input.Split("\n\n")[1]
                .Split('\n')
                .Select(ulong.Parse)
                .ToList();
                
            return (ranges, numbers);
        }
    }
}