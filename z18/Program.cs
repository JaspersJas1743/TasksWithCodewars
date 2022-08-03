using System;
using System.Linq;
using System.Collections.Generic;

/*
n this kata we want to convert a string into an integer. The strings simply represent the numbers in words.

Examples:

"one" => 1
"twenty" => 20
"two hundred forty-six" => 246
"seven hundred eighty-three thousand nine hundred nineteen" => 783919
Additional Notes:

The minimum number is "zero" (inclusively)
The maximum number, which must be supported is 1 million (inclusively)
The "and" in e.g. "one hundred and twenty-four" is optional, in some cases it's present and in others it's not
All tested numbers are valid, you don't need to validate them
*/

class Parser
{
    private static List<string> _starts = new List<string>(){"on", "tw", "th", "fo", "fi", "si", "se", "ei", "ni", "te", "el"};

    public static int ParseInt(string? s)
    {
        if (string.IsNullOrEmpty(s)) throw new Exception();

        int result = 0, lastNum = 0;

        foreach (var word in s.ToLower().Replace(" and", "").Split(new char[]{' ', '-'}, StringSplitOptions.TrimEntries))
        {
            int num = _starts.IndexOf(word[0..2]) + 1;
            if (new string[]{"een", "twelve"}.Any(end => word.EndsWith(end)))
                num += 10;
            else if (word.EndsWith("ty"))
                num *= 10;
            
            switch (word)
            {
                case "hundred":  result += lastNum * 100 - lastNum; break; 
                case "thousand": result = result * 1000 - 3; break;
                case "million":  result *= 1000000; break;
            }
            lastNum = num;
            result += num;  
        }

        return result;
    }
}

class Program
{
    public static void Main(string[] args) => Console.WriteLine(Parser.ParseInt(Console.ReadLine()));
}