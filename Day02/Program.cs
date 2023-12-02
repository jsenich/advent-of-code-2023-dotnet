
using System.Text.RegularExpressions;

internal class Program
{

    static Dictionary<string, int> totalCubes = new Dictionary<string, int>(){
            {"red", 12},
            {"green", 13},
            {"blue", 14},
        };

    static string cubePattern = @"^(?:Game (?<gameId>\d+)){1}: (?:(?<cubes>\d+ [a-z]+)[,;]? ?)+";

    static int PartOne(string[] puzzleInput)
    {
        int total = 0;

        foreach (var game in puzzleInput)
        {
            var matches = Regex.Matches(game, cubePattern);
            bool isOver = false;
            foreach (Match m in matches)
            {
                var gameId = int.Parse(m.Groups["gameId"].Value);

                foreach (Capture cubes in m.Groups["cubes"].Captures)
                {
                    var parts = cubes.Value.Split(" ");
                    if (int.Parse(parts[0]) > totalCubes[parts[1]])
                    {
                        isOver = true;
                        break;
                    }
                }
                if (!isOver)
                {
                    total += gameId;
                }
            }
        }

        return total;
    }

    static int PartTwo(string[] puzzleInput)
    {

        int total = 0;

        foreach (var game in puzzleInput)
        {
            var matches = Regex.Matches(game, cubePattern);

            Dictionary<string, List<int>> cubeSets = new Dictionary<string, List<int>>(){
                    {"red", new List<int>()},
                    {"green", new List<int>()},
                    {"blue", new List<int>()},
                };

            foreach (Match m in matches)
            {
                foreach (Capture cubes in m.Groups["cubes"].Captures)
                {
                    var parts = cubes.Value.Split(" ");
                    cubeSets[parts[1]].Add(int.Parse(parts[0]));
                }
            }
            total += cubeSets["red"].Max() * cubeSets["blue"].Max() * cubeSets["green"].Max();
        }

        return total;
    }

    private static void Main()
    {
        //         var puzzleInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        // Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
        // Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
        // Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
        // Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
        // ".Trim().Split("\n");

        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 2541
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 6601
    }
}
