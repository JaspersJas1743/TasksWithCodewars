using System;
using System.Linq;
using System.Collections.Generic;

/*
Write a program that will calculate the number of trailing zeros in a factorial of a given number.
Be careful 1000! has 2568 digits...
For more info, see: http://mathworld.wolfram.com/Factorial.html
Examples
zeros(6) = 1 => 6! = 1 * 2 * 3 * 4 * 5 * 6 = 720 --> 1 trailing zero
zeros(12) = 2 => 12! = 479001600 --> 2 trailing zeros
Think: You're not meant to calculate the factorial. Find another way to find the number of zeros.
*/

namespace z15
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Kata.TrailingZeros(5));
            Console.WriteLine(Kata.TrailingZeros(25));
            Console.WriteLine(Kata.TrailingZeros(531));
        }
    }

    public static class Kata 
    {
        public static int TrailingZeros(int numberToCheck)
        {
            if (numberToCheck < 0) return -1;
            int result = 0;
            for (int i = 5; numberToCheck / i >= 1; i *= 5)
                result += numberToCheck / i;
            
            return result;
        }
    }
}