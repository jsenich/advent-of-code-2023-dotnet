using System.Diagnostics.Metrics;

internal class Program
{
    private static int PartOne(string[] puzzleInput)
    {
        var grid = new List<List<char>>();
        var length = puzzleInput[0].Length;
        var galaxyCols = new HashSet<int>();
        var colNums = Enumerable.Range(0, length).ToHashSet();
        var galaxies = new List<(int, int)>();


        foreach (var line in puzzleInput)
        {
            var currLine = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                currLine.Add(line[i]);
                if (line[i] == '#')
                {
                    galaxyCols.Add(i);
                }
            }
            if (!line.Contains('#'))
            {
                var extraLine = new List<char>();

                extraLine.AddRange(new string('.', length));
                grid.Add(extraLine);
            }
            grid.Add(currLine);
        }

        var toExpand = colNums.Except(galaxyCols).Order().ToList();

        foreach (var line in grid)
        {
            var incrementer = 0;
            foreach (var colNum in toExpand)
            {
                line.Insert(colNum + incrementer + 1, '.');
                incrementer++;
            }
        }

        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                if (grid[i][j] == '#')
                {
                    galaxies.Add((i, j));
                }
            }
        }


        var total = 0;

        var counter = 0;
        foreach (var point in galaxies)
        {
            foreach (var point2 in galaxies.Skip(counter + 1))
            {
                total += Math.Abs((point.Item1 - point2.Item1)) + Math.Abs((point.Item2 - point2.Item2));
            }
            counter++;
        }

        return total;
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 10033566
    }
}
