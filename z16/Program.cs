using System;
using System.Linq;
using System.Collections.Generic;

/*
Create a Roman Numerals class that can convert a roman numeral to and from an integer value.
It should follow the API demonstrated in the examples below. Multiple roman numeral
values will be tested for each helper method.

Modern Roman numerals are written by expressing each digit separately starting with
the leftmost digit and skipping any digit with a value of zero. In Roman numerals 
1990 is rendered: 1000=M, 900=CM, 90=XC; resulting in MCMXC.
2008 is written as 2000=MM, 8=VIII; or MMVIII.
1666 uses each Roman symbol in descending order: MDCLXVI.

Input range : 1 <= n < 4000
In this kata 4 should be represented as IV, NOT as IIII (the "watchmaker's four").
*/

namespace z16
{
    class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class RomanNumerals
    {
        private static Dictionary<char, int> _romanNumerals = new Dictionary<char, int>(){
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50},
            {'C', 100}, {'D', 500}, {'M', 1000},
        };

        private static Dictionary<int, string> _arabianNumerals = new Dictionary<int, string>() {
            {1000, "M"}, {900, "CM"}, {500, "D"},
            {400, "CD"}, {100, "C"}, {90, "XC"},
            {50, "L"}, {40, "XL"}, {10, "X"},
            {9, "IX"}, {5, "V"}, {4, "IV"}, {1, "I"},
        };
    
        public static string ToRoman(int arabianNumerals)
        {
            if (arabianNumerals <= 0 || arabianNumerals >= 4000) return string.Empty;

            string result = "";
            foreach (var elem in _arabianNumerals)
            {
                while (arabianNumerals >= elem.Key) {
                    arabianNumerals -= elem.Key;
                    result += elem.Value;
                }
            }

            return result;
        }

        public static int FromRoman(string romanNumeral)
        {
            int result = _romanNumerals[romanNumeral.Last()];
            for (int index = 1; index < romanNumeral.Length; ++index)
            {
                int previous = _romanNumerals[romanNumeral[index - 1]];
                int current = _romanNumerals[romanNumeral[index]];
                bool exp10 = (Math.Log10(previous) - Math.Truncate(Math.Log10(previous))) == 0;
                bool nearestNumericalSeries = Enumerable.Range(previous, previous * 9 + 1).Contains(current);
                if (previous < current && exp10 && nearestNumericalSeries) result -= previous;
                else result += previous;
            }
            return result;
        }
    }
}