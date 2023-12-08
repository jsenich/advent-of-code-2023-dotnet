using System.ComponentModel.DataAnnotations;
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

        long steps = 0;

        var startNodes = nodeMap.Keys.Where(x => x.EndsWith("A")).ToList();
        var stopNodes = nodeMap.Keys.Where(x => x.EndsWith("Z")).ToList();

        var currentNodes = new List<string>(startNodes);

        while (true)
        {
            foreach (var d in directions)
            {
                if (d == 'L')
                {
                    currentNodes[0] = nodeMap[currentNodes[0]].Item1;
                    currentNodes[1] = nodeMap[currentNodes[1]].Item1;
                    currentNodes[2] = nodeMap[currentNodes[2]].Item1;
                    currentNodes[3] = nodeMap[currentNodes[3]].Item1;
                    currentNodes[4] = nodeMap[currentNodes[4]].Item1;
                    currentNodes[5] = nodeMap[currentNodes[5]].Item1;
                }
                else
                {
                    currentNodes[0] = nodeMap[currentNodes[0]].Item2;
                    currentNodes[1] = nodeMap[currentNodes[1]].Item2;
                    currentNodes[2] = nodeMap[currentNodes[2]].Item2;
                    currentNodes[3] = nodeMap[currentNodes[3]].Item2;
                    currentNodes[4] = nodeMap[currentNodes[4]].Item2;
                    currentNodes[5] = nodeMap[currentNodes[5]].Item2;
                }
                steps++;
                if (currentNodes.Intersect(stopNodes).ToList().Count == 6)
                {
                    return steps;
                }
            }
        }
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n\n");

        // Console.WriteLine($"Part One: {PartOne(puzzleInput[0], puzzleInput[1])}"); // 19631
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput[0], puzzleInput[1])}"); //

    }
}
