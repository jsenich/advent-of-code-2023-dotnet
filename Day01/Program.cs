namespace Day01;

using System.Text.RegularExpressions;

internal class Program
{
    static string ConvertValue(string param)
    {
        string myval;

        switch (param)
        {
            case "one":
                myval = "1";
                break;
            case "two":
                myval = "2";
                break;
            case "three":
                myval = "3";
                break;
            case "four":
                myval = "4";
                break;
            case "five":
                myval = "5";
                break;
            case "six":
                myval = "6";
                break;
            case "seven":
                myval = "7";
                break;
            case "eight":
                myval = "8";
                break;
            case "nine":
                myval = "9";
                break;
            default:
                myval = param;
                break;
        }

        return myval;
    }

    static int PartOne(string[] puzzleInput)
    {
        int calibrationTotal = 0;
        foreach (var line in puzzleInput)
        {
            int calibrationValue;
            var digits = Regex.Matches(line, "[1-9]{1}");

            if (digits.Count > 1)
            {
                calibrationValue = int.Parse(digits[0].Value + digits[digits.Count-1].Value);
            }
            else
            {
                calibrationValue = int.Parse(digits[0].Value + digits[0].Value);
            }

            calibrationTotal += calibrationValue;
        }

        return calibrationTotal;
    }

    static int PartTwo(string[] puzzleInput)
    {
        int calibrationTotal = 0;
        foreach (var line in puzzleInput)
        {
            int calibrationValue;
            var escapedLine = line.Replace("one", "o1e")
            .Replace("two", "t2o")
            .Replace("three", "t3e")
            .Replace("four", "f4r")
            .Replace("five", "f5e")
            .Replace("six", "s6x")
            .Replace("seven", "s7n")
            .Replace("eight", "e8t")
            .Replace("nine", "n9e");
            var digits = Regex.Matches(escapedLine, "[1-9]{1}|one{1}|two{1}|three{1}|four{1}|five{1}|six{1}|seven{1}|eight{1}|nine{1}");

            if (digits.Count > 1)
            {
                calibrationValue = int.Parse(ConvertValue(digits[0].Value) + ConvertValue(digits[digits.Count-1].Value));
            }
            else
            {
                calibrationValue = int.Parse(ConvertValue(digits[0].Value) + ConvertValue(digits[0].Value));
            }

            calibrationTotal += calibrationValue;
        }

        return calibrationTotal;
    }

    private static void Main()
    {
        var puzzleInput = File.ReadAllText("input.txt").Trim().Split("\n");

        Console.WriteLine($"Part One: {PartOne(puzzleInput)}");
        Console.WriteLine($"Part Two: {PartTwo(puzzleInput)}");
    }
}
