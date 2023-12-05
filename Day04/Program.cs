using System.Text.RegularExpressions;

internal class Program
{
    static int PartOne(string[] puzzleInput)
    {
        var pattern = @"^(?:Card +\d+: +)(?:(?<winningNums>\d+)(?: +)?)+(?:\| +){1}(?:(?<yourNums>\d+)(?: +)?)+";

        int totalPoints = 0;
        foreach (var line in puzzleInput)
        {
            var m = Regex.Match(line, pattern);

            var winningNums = m.Groups["winningNums"].Captures.Select(x => x.Value).ToList();
            var yourNums = m.Groups["yourNums"].Captures.Select(x => x.Value).ToList();

            int points = 0;
            int i = 1;
            foreach (var val in winningNums.Intersect(yourNums))
            {
                if (i == 1)
                {
                    points = 1;
                    i++;
                    continue;
                }
                points = points * 2;
            }
            totalPoints += points;

        }

        return totalPoints;
    }

    static int PartTwo(string[] puzzleInput)
    {
        var scratchCards = puzzleInput.ToList<string>();
        Dictionary<int, int> copyCounts = scratchCards.Select((_, index) => index).ToDictionary(k => k, ValueMatch => 1);
        var winningCounts = new Dictionary<int, int>();
        var pattern = @"^(?:Card +(?<cardNum>\d+): +)(?:(?<winningNums>\d+)(?: +)?)+(?:\| +){1}(?:(?<yourNums>\d+)(?: +)?)+";

        var scratchCardsCount = scratchCards.Count();

        for (var i = 0; i < scratchCardsCount; i++)
        {
            var m = Regex.Match(scratchCards[i], pattern);

            var winningNums = m.Groups["winningNums"].Captures.Select(x => x.Value).ToList();
            var yourNums = m.Groups["yourNums"].Captures.Select(x => x.Value).ToList();


            var matchingCount = winningNums.Intersect(yourNums).ToList().Count;
            if (matchingCount == 0)
            {
                continue;
            }
            var currentCount = copyCounts[i];


            foreach (var j in Enumerable.Range(i + 1, matchingCount))
            {
                if (j < scratchCardsCount)
                {
                    copyCounts[j] += currentCount;
                }
            }

        }

        return copyCounts.Values.Sum();
    }

    private static void Main(string[] args)
    {
        //         var puzzleInput = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        // Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
        // Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
        // Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
        // Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
        // Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
        // ".Trim().Split("\n");

        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 27845
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 9496801
    }
}
