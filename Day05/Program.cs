using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml;

internal class Program
{
    private static ulong PartOne(string[] almanac)
    {
        ulong lowestNum = 9999999999;

        var maps = new Dictionary<int, List<(ulong dest, ulong src, ulong count)>>();
        var seeds = Regex.Matches(almanac[0], @"\d+").Select(m => ulong.Parse(m.Value)).ToList();

        for (int i = 1; i < almanac.Length; i++)
        {
            List<(ulong, ulong, ulong)> destSourceRange = Regex.Matches(almanac[i], @"\d+ \d+ \d+").Select(
                m => m.Value.Split(" ")
                switch
                { var s => (ulong.Parse(s[0]), ulong.Parse(s[1]), ulong.Parse(s[2])) }
                ).ToList();

            maps[i] = new List<(ulong dest, ulong src, ulong count)>();
            foreach (var s in destSourceRange)
            {
                maps[i].Add((s.Item1, s.Item2, s.Item3));
            }
        }

        foreach (var seed in seeds)
        {
            var currMap = 1;
            var currKey = seed;
            while (currMap < maps.Count + 1)
            {
                var myMap = maps[currMap];

                foreach (var numRange in myMap)
                {
                    if (currKey >= numRange.src && currKey <= (numRange.src + numRange.count))
                    {
                        currKey = numRange.dest + (currKey - numRange.src);
                        break;
                    }
                }

                currMap++;
            }
            if (currKey < lowestNum)
            {
                lowestNum = currKey;
            }
        }

        return lowestNum;
    }

    private static ulong PartTwo(string[] puzzleInput)
    {
        throw new NotImplementedException();
    }

    public static IEnumerable<ulong> Range(ulong start, ulong count)
    {
        for (ulong i = 0; i < count; i++) yield return start + i;
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n\n");


        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 382895070
    }
}
