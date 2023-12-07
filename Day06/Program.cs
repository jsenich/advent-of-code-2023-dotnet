using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

internal class Program
{
    static int PartOne(string[] puzzleInput)
    {
        var pattern = @"(?<num>\d+ *)+";

        var times = Regex.Match(puzzleInput[0], pattern)
            .Groups["num"]
            .Captures
            .Select(c => int.Parse(c.Value)).ToList();

        var distances = Regex.Match(puzzleInput[1], pattern)
            .Groups["num"]
            .Captures
            .Select(c => int.Parse(c.Value)).ToList();

        var raceMap = times.Zip(distances, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);

        var winningCounts = new List<int>();

        foreach ((int time, int distance) in raceMap)
        {
            int wins = 0;

            foreach (int x in Enumerable.Range(2, time - 1))
            {
                var d = (time - x) * x;
                if (d > distance)
                {
                    wins++;
                }
            }
            winningCounts.Add(wins);
        }

        return winningCounts.Aggregate((x, y) => x * y);
    }

    static int PartTwo(string[] puzzleInput)
    {
        var pattern = @"(?<num>\d+ *){1}";


        var times = Regex.Match(puzzleInput[0].Replace(" ", ""), pattern)
            .Groups["num"]
            .Captures
            .Select(c => long.Parse(c.Value)).ToList();

        var distances = Regex.Match(puzzleInput[1].Replace(" ", ""), pattern)
            .Groups["num"]
            .Captures
            .Select(c => long.Parse(c.Value)).ToList();

        var raceMap = times.Zip(distances, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v);

        var winningCounts = new List<int>();

        foreach ((long time, long distance) in raceMap)
        {
            int wins = 0;

            for (long x = 2; x < time; x++)
            {
                var d = (time - x) * x;
                if (d > distance)
                {
                    wins++;
                }
            }
            winningCounts.Add(wins);
        }

        return winningCounts.Aggregate((x, y) => x * y);
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 5133600
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 40651271
    }
}
