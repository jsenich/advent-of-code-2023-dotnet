using System.Net.Sockets;

internal class Program
{
    static int PartOne(string[] puzzleInput)
    {
        return 0;
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

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



        Console.WriteLine($"Part One: {historyValues.Sum()}"); // Answer: 2043677056
    }
}
