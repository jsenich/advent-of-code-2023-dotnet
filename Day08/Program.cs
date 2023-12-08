using System.Text.RegularExpressions;

internal class Program
{
    static string nodePattern = @"^(?<key>[A-Z]{3}) = \((?<left>[A-Z]{3}), (?<right>[A-Z]{3})\)$";

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

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput[0], puzzleInput[1])}"); // 19631
    }
}
