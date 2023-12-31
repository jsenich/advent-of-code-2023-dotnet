using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class HandComparer : IComparer<string>
{
    Dictionary<string, int> _ranks;

    public HandComparer(Dictionary<string, int> ranks)
    {
        _ranks = ranks;
    }

    public int Compare(string? x, string? y)
    {
        foreach (var (a, b) in x.Zip(y, (a, b) => (a.ToString(), b.ToString())))
        {
            if (_ranks[a] == _ranks[b])
            {
                continue;
            }

            return _ranks[a].CompareTo(_ranks[b]);

        }

        return 1;
    }
}

internal class Program
{
    static long PartOne(string[] puzzleInput)
    {
        Dictionary<string, int> cardRanks = new Dictionary<string, int>{
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

        var fiveOfAKind = new List<string>();
        var fourOfAKind = new List<string>();
        var fullHouse = new List<string>();
        var threeOfAKind = new List<string>();
        var twoPair = new List<string>();
        var onePair = new List<string>();
        var highCard = new List<string>();

        var handBids = new Dictionary<string, int>();

        foreach (var line in puzzleInput)
        {
            (string hand, int bid) = line.Split(" ")
            switch
            { var l => (l[0], int.Parse(l[1])) };

            handBids[hand] = bid;

            var counts = hand.GroupBy(c => c).Select(g => g.Count());

            if (counts.Contains(5))
            {
                fiveOfAKind.Add(hand);
            }
            else if (counts.Contains(4))
            {
                fourOfAKind.Add(hand);
            }
            else if (counts.Contains(3) && counts.Contains(2))
            {
                fullHouse.Add(hand);
            }
            else if (counts.Contains(3))
            {
                threeOfAKind.Add(hand);
            }
            else if (counts.Count(c => c.Equals(2)) == 2)
            {
                twoPair.Add(hand);
            }
            else if (counts.Contains(2))
            {
                onePair.Add(hand);
            }
            else
            {
                highCard.Add(hand);
            }
        }

        if (fiveOfAKind.Count > 1)
        {
            fiveOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (fourOfAKind.Count > 1)
        {
            fourOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (twoPair.Count > 1)
        {
            twoPair.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (fullHouse.Count > 1)
        {
            fullHouse.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (threeOfAKind.Count > 1)
        {
            threeOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (onePair.Count > 1)
        {
            onePair.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (highCard.Count > 1)
        {
            highCard.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }

        List<string> sortedCards =
        [
            .. fiveOfAKind,
            .. fourOfAKind,
            .. fullHouse,
            .. threeOfAKind,
            .. twoPair,
            .. onePair,
            .. highCard,
        ];
        sortedCards.Reverse();

        long total = 0;
        for (int i = 0; i < sortedCards.Count; i++)
        {
            total += (i + 1) * handBids[sortedCards[i]];
        }

        return total;
    }

    static long PartTwo(string[] puzzleInput)
    {
        Dictionary<string, int> cardRanks = new Dictionary<string, int>{
            {"A", 14},
            {"K", 13},
            {"Q", 12},
            {"T", 10},
            {"9", 9},
            {"8", 8},
            {"7", 7},
            {"6", 6},
            {"5", 5},
            {"4", 4},
            {"3", 3},
            {"2", 2},
            {"J", 1},
        };

        var fiveOfAKind = new List<string>();
        var fourOfAKind = new List<string>();
        var fullHouse = new List<string>();
        var threeOfAKind = new List<string>();
        var twoPair = new List<string>();
        var onePair = new List<string>();
        var highCard = new List<string>();

        var handBids = new Dictionary<string, int>();

        foreach (var line in puzzleInput)
        {
            (string hand, int bid) = line.Split(" ")
            switch
            { var l => (l[0], int.Parse(l[1])) };

            handBids[hand] = bid;

            var handCopy = hand;

            var counts = hand.GroupBy(c => c).Select(g => new { K = g.Key.ToString(), V = g.Count() }).ToDictionary(x => x.K, x => x.V);


            if (counts.Any(c => c.Value == 5))
            {
                fiveOfAKind.Add(hand);
            }
            else if (counts.Any(c => c.Value == 4))
            {
                if (!counts.ContainsKey("J"))
                {
                    fourOfAKind.Add(hand);
                }
                else
                {
                    fiveOfAKind.Add(hand);
                }
            }
            else if (counts.Any(c => c.Value == 3))
            {
                if (counts.Any(c => c.Value == 2))
                {
                    if (!counts.ContainsKey("J"))
                    {
                        fullHouse.Add(hand);
                    }
                    else
                    {
                        var jacks = counts["J"];
                        if (jacks == 2 || jacks == 3)
                        {
                            fiveOfAKind.Add(hand);
                        }
                        else
                        {
                            fourOfAKind.Add(hand);
                        }
                    }
                }
                else
                {
                    if (!counts.ContainsKey("J"))
                    {
                        threeOfAKind.Add(hand);
                    }
                    else
                    {
                        var jacks = counts["J"];
                        if (jacks == 3 || jacks == 1)
                        {
                            fourOfAKind.Add(hand);
                        }
                    }
                }
            }
            else if (counts.Any(c => c.Value == 2))
            {
                var twos = counts.Where(c => c.Value == 2).Select(s => s.Key).ToList();
                if (twos.Count > 1)
                {
                    if (!counts.ContainsKey("J"))
                    {
                        twoPair.Add(hand);
                    }
                    else
                    {
                        if (twos.Contains("J"))
                        {
                            fourOfAKind.Add(hand);
                        }
                        else
                        {
                            fullHouse.Add(hand);
                        }
                    }
                }
                else
                {
                    if (!counts.ContainsKey("J"))
                    {
                        onePair.Add(hand);
                    }
                    else
                    {
                        threeOfAKind.Add(hand);
                    }
                }
            }
            else
            {
                if (!counts.ContainsKey("J"))
                {
                    highCard.Add(hand);
                }
                else
                {
                    onePair.Add(hand);
                }
            }
        }

        if (fiveOfAKind.Count > 1)
        {
            fiveOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (fourOfAKind.Count > 1)
        {
            fourOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (twoPair.Count > 1)
        {
            twoPair.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (fullHouse.Count > 1)
        {
            fullHouse.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (threeOfAKind.Count > 1)
        {
            threeOfAKind.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (onePair.Count > 1)
        {
            onePair.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }
        if (highCard.Count > 1)
        {
            highCard.Sort((a, b) => new HandComparer(cardRanks).Compare(b, a));
        }

        List<string> sortedCards =
        [
            .. fiveOfAKind,
            .. fourOfAKind,
            .. fullHouse,
            .. threeOfAKind,
            .. twoPair,
            .. onePair,
            .. highCard,
        ];
        sortedCards.Reverse();

        long total = 0;
        for (int i = 0; i < sortedCards.Count; i++)
        {
            total += (i + 1) * handBids[sortedCards[i]];
        }

        return total;
    }

    private static void Main(string[] args)
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");


        Console.WriteLine($"Part One: {PartOne(puzzleInput)}"); // Answer: 248396258
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}"); // Answer: 246436046
    }
}
