using System.Text.RegularExpressions;

internal class Program
{
    static string nodePattern = @"^(?<key>[1-9A-Z]{3}) = \((?<left>[1-9A-Z]{3}), (?<right>[1-9A-Z]{3})\)$";

    static int PartOne(string directions, string instructions)
    {
        string startNode;
        string stopNode;

        var matches = Regex.Matches(instructions, nodePattern, RegexOptions.Multiline);
        startNode = "AAA";
        stopNode = "ZZZ";

        var nodeMap = matches.Select(
            m => new
            {
                Key = m.Groups["key"].Value,
                Value = (m.Groups["left"].Value, m.Groups["right"].Value)
            }
            ).ToDictionary(x => x.Key, x => x.Value);

        int steps = 0;
        var currentNode = startNode;

        while (currentNode != stopNode)
        {
            foreach (var d in directions)
            {
                if (d == 'L')
                {
                    currentNode = nodeMap[currentNode].Item1;
                }
                else
                {
                    currentNode = nodeMap[currentNode].Item2;
                }
                steps++;
                if (currentNode == stopNode)
                {
                    break;
                }
            }
        }

        return steps;
    }

    static long gcd(long n1, long n2)
    {
        if (n2 == 0)
        {
            return n1;
        }
        else
        {
            return gcd(n2, n1 % n2);
        }
    }

    static long PartTwo(string directions, string instructions)
    {
        var matches = Regex.Matches(instructions, nodePattern, RegexOptions.Multiline);

        var nodeMap = matches.Select(
            m => new
            {
                Key = m.Groups["key"].Value,
                Value = (m.Groups["left"].Value, m.Groups["right"].Value)
            }
            ).ToDictionary(x => x.Key, x => x.Value);

        var startNodes = nodeMap.Keys.Where(x => x.EndsWith("A")).ToList();
        var totals = new List<long>();

        foreach (var startNode in startNodes)
        {
            int steps = 0;
            var currentNode = startNode;

            while (!currentNode.EndsWith("Z"))
            {
                foreach (var d in directions)
                {
                    if (d == 'L')
                    {
                        currentNode = nodeMap[currentNode].Item1;
                    }
                    else
                    {
                        currentNode = nodeMap[currentNode].Item2;
                    }
                    steps++;
                    if (currentNode.EndsWith("Z"))
                    {
                        break;
                    }
                }
            }
            totals.Add(steps);
        }

        return totals.Aggregate((S, val) => S * val / gcd(S, val));
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput[0], puzzleInput[1])}"); // 19631
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput[0], puzzleInput[1])}"); // 21003205388413
    }
}
