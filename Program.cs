var input = File.ReadAllText("../../../data.txt");
input = input.Replace("\r", ""); // Normalize line endings for Windows

var ranges = input.Split("\n\n")[0]
    .Split('\n')
    .Select(line => line.Split('-'))
    .Select(parts => (Start: long.Parse(parts[0]), End: long.Parse(parts[1])))
    .ToList();
    
var numbers = input.Split("\n\n")[1]
    .Split('\n')
    .Select(long.Parse)
    .ToList();
    
var sortedByStart = ranges.OrderBy(r => r.Start).ToList();
var mergedRanges = new List<(long Start, long End)>();

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

var countInRanges = numbers.Count(number => mergedRanges.Any(r => number >= r.Start && number <= r.End));

Console.WriteLine("Count of numbers within ranges:\n" + countInRanges);