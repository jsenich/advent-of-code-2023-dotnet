using System.Net.Sockets;

internal class Program
{
    static long PartOne(string[] puzzleInput)
    {
        var historyValues = new List<long>();
        int lineNum = 0;
        foreach (var line in puzzleInput)
        {
            var nums = line.Split(" ").Select(n => long.Parse(n)).ToList();
            var histories = new List<List<long>>() { nums };

            int i = 0;

            while (true)
            {
                histories.Add(new List<long>());
                for (int j = 0; j < histories[i].Count - 1; j++)
                {
                    histories[i + 1].Add(histories[i][j + 1] - histories[i][j]);
                }

                if (histories[i + 1].All(x => x == 0))
                {
                    break;
                }
                i++;
            }

            for (int x = histories.Count - 1; x > 0; x--)
            {
                histories[x - 1].Add(histories[x - 1].Last() + histories[x].Last());
                if (x - 1 == 0)
                {
                    historyValues.Add(histories[x - 1].Last());
                    break;
                }
            }
            lineNum++;
        }

        return historyValues.Sum();
    }

    static long PartTwo(string[] puzzleInput)
    {
        var historyValues = new List<long>();
        foreach (var line in puzzleInput)
        {
            var nums = line.Split(" ").Select(n => long.Parse(n)).ToList();
            var histories = new List<List<long>>() { nums };

            int i = 0;

            while (true)
            {
                histories.Add(new List<long>());
                for (int j = 0; j < histories[i].Count - 1; j++)
                {
                    histories[i + 1].Add(histories[i][j + 1] - histories[i][j]);
                }


                if (histories[i + 1].All(x => x == 0))
                {
                    break;
                }
                i++;
            }

            for (int x = histories.Count - 1; x > 0; x--)
            {
                histories[x - 1].Insert(0, histories[x - 1][0] - histories[x][0]);
                if (x - 1 == 0)
                {
                    historyValues.Add(histories[x - 1][0]);
                    break;
                }
            }
        }

        return historyValues.Sum();
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 2043677056
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 1062


    }
}
