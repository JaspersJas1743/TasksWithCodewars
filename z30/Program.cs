using System;
using System.Linq;
using System.Collections.Generic;

#region Exercise
/*
The task is simply stated. Given an integer n (3 < n < 109), find the length of the smallest
list of perfect squares which add up to n. Come up with the best algorithm you can; you'll need it!
Examples:
sum_of_squares(17) = 2
17 = 16 + 1 (16 and 1 are perfect squares).
sum_of_squares(15) = 4
15 = 9 + 4 + 1 + 1. There is no way to represent 15 as the sum of three perfect squares.
sum_of_squares(16) = 1
16 itself is a perfect square.
Time constraints:
5 easy (sample) test cases: n < 20
5 harder test cases: 1000 < n < 15000
5 maximally hard test cases: 5e8 < n < 1e9
*/
#endregion

namespace z30
{
    public class SumOfSquares
    { 
        public static int NSquaresFor(int n)
        {
            double root = Math.Sqrt(n);
            if (root == (int)root) return 1;
            
            if (Enumerable.Range(1, (int)Math.Truncate(Math.Sqrt(n))).Select(x => Math.Sqrt(n - Math.Pow(x, 2))).Where(x => x == (int)x).Count() > 0)
                return 2;

            while (n % 4 == 0) n /= 4;
            if (n % 8 == 7) return 4;

            return 3;
        }
    }

    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"SumOfSquares.NSquaresFor(15) = {SumOfSquares.NSquaresFor(15)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(16) = {SumOfSquares.NSquaresFor(16)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(17) = {SumOfSquares.NSquaresFor(17)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(18) = {SumOfSquares.NSquaresFor(18)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(19) = {SumOfSquares.NSquaresFor(19)}");
            
            Console.WriteLine($"SumOfSquares.NSquaresFor(225835646) = {SumOfSquares.NSquaresFor(225835646)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(661915703) = {SumOfSquares.NSquaresFor(661915703)}");
            Console.WriteLine($"SumOfSquares.NSquaresFor(1008) = {SumOfSquares.NSquaresFor(1008)}");
        }
    }
}