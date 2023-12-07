internal class Program
{

    static Dictionary<string, int> cardRanks = new Dictionary<string, int>{
        {"A", 14},
        {"K", 13},
        {"Q", 12},
        {"J", 11},
        {"T", 10},
        {"9", 9},
        {"8", 8},
        {"7", 7},
        {"6", 6},
        {"5", 5},
        {"4", 4},
        {"3", 3},
        {"2", 2},
    };

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("sample_input.txt").Trim().Split("\n");

        foreach (var line in puzzleInput)
        {
            (string hand, int bid) = line.Split(" ")
            switch
            { var l => (l[0], int.Parse(l[1])) };

        }


        Console.WriteLine("Hello, World!");
    }
}
