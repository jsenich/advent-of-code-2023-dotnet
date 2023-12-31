using System.Text.RegularExpressions;

internal class Program
{

    static int PartOne(string[] puzzleInput)
    {
        string numPattern = @"(?<partNum>\d+)*(?<symbol>[^0-9\n\.])?";

        int lineNum = 0;
        Dictionary<int, List<(string, int)>> parntNums = new Dictionary<int, List<(string, int)>>();
        var symbolLocs = new List<(int row, int col)>();
        foreach (var row in puzzleInput)
        {
            parntNums[lineNum] = new List<(string, int)>();
            var matches = Regex.Matches(row, numPattern);
            foreach (Match m in matches)
            {
                foreach (Capture nums in m.Groups["partNum"].Captures)
                {
                    parntNums[lineNum].Add(new(nums.Value, nums.Index));
                }
                foreach (Capture symbol in m.Groups["symbol"].Captures)
                {
                    symbolLocs.Add(new(lineNum, symbol.Index));
                }
            }
            lineNum++;
        }

        int partTotals = 0;
        for (var i = 0; i < lineNum; i++)
        {
            foreach (var (val, index) in parntNums[i])
            {
                var coords = new List<(int, int)>();
                coords.Add((i, index - 1));
                coords.Add((i, index + val.Length));
                for (var j = index - 1; j <= index + val.Length; j++)
                {
                    coords.Add((i - 1, j));
                }
                for (var j = index - 1; j <= index + val.Length; j++)
                {
                    coords.Add((i + 1, j));
                }

                if (coords.Intersect(symbolLocs).Any())
                {
                    partTotals += int.Parse(val);
                }
            }
        }
        return partTotals;
    }

    static int PartTwo(string[] puzzleInput)
    {
        string numPattern = @"(?<partNum>\d+)*(?<gear>[/*])?";

        int lineNum = 0;
        Dictionary<int, List<(string, int)>> parntNums = new Dictionary<int, List<(string, int)>>();
        var gearLocs = new List<(int row, int col)>();
        foreach (var row in puzzleInput)
        {
            parntNums[lineNum] = new List<(string, int)>();
            var matches = Regex.Matches(row, numPattern);
            foreach (Match m in matches)
            {
                foreach (Capture nums in m.Groups["partNum"].Captures)
                {
                    parntNums[lineNum].Add(new(nums.Value, nums.Index));
                }
                foreach (Capture symbol in m.Groups["gear"].Captures)
                {
                    gearLocs.Add(new(lineNum, symbol.Index));
                }
            }
            lineNum++;
        }

        int gearRatios = 0;
        var gearIntersections = new Dictionary<(int, int), List<int>>();
        for (var i = 0; i < lineNum; i++)
        {
            foreach (var (val, index) in parntNums[i])
            {
                var coords = new List<(int, int)>();
                coords.Add((i, index - 1));
                coords.Add((i, index + val.Length));
                for (var j = index - 1; j <= index + val.Length; j++)
                {
                    coords.Add((i - 1, j));
                }
                for (var j = index - 1; j <= index + val.Length; j++)
                {
                    coords.Add((i + 1, j));
                }


                var ratios = coords.Intersect(gearLocs).ToList();
                if (ratios.Count == 1)
                {
                    if (!gearIntersections.ContainsKey(ratios[0]))
                    {
                        gearIntersections[ratios[0]] = new List<int>();
                    }
                    gearIntersections[ratios[0]].Add(int.Parse(val));
                }
            }
        }

        foreach (KeyValuePair<(int, int), List<int>> gear in gearIntersections)
        {
            if (gear.Value.Count == 2)
            {
                gearRatios += gear.Value[0] * gear.Value[1];
            }
        }

        return gearRatios;
    }

    private static void Main(string[] args)
    {
        //         var puzzleInput = @"467..114..
        // ...*......
        // ..35..633.
        // ......#...
        // 617*......
        // .....+.58.
        // ..592.....
        // ......755.
        // ...$.*....
        // .664.598..".Trim().Split("\n");

        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 517021
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 81296995
    }
}
