using System;
using System.Linq;
using System.Collections.Generic;

/*
A format for expressing an ordered list of integers is to use a comma separated list of either
1. individual integers
2. or a range of integers denoted by the starting integer separated from the end integer in the
range by a dash, '-'. The range includes all integers in the interval including both endpoints.
It is not considered a range unless it spans at least 3 numbers. For example "12,13,15-17"

Complete the solution so that it takes a list of integers in increasing order and returns
a correctly formatted string in the range format.

Example:
solution([-10, -9, -8, -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20]);
returns "-10--8,-6,-3-1,3-5,7-11,14,15,17-20"
Courtesy of rosettacode.org
*/

public class RangeExtraction
{
    public static string Extract(int[] args)
    {
        if (args.Length == 1) return args.First().ToString();
        else if (args.Length == 2) return $"{args.First()},{args.Last()}";

        string result = "";
        int lastNumber = args.First(), countQueue = 1;

        for (int index = 1; index <= args.Length; ++index)
        {
            int currentNumber = args.ElementAtOrDefault(index);
            
            if (currentNumber - lastNumber == 1) ++countQueue;
            else {
                switch (countQueue)
                {
                    case 1: result += $",{lastNumber}"; break;
                    case 2: result += $",{args[index - countQueue]},{lastNumber}"; break;
                    default: result += $",{args[index - countQueue]}-{lastNumber}"; break;
                }
                countQueue = 1;
            }

            lastNumber = currentNumber;
        }

        return result[1..^0];
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Console.WriteLine(RangeExtraction.Extract(new int[]{1, 2}));
        // Console.WriteLine(RangeExtraction.Extract(new int[]{1, 2, 3}));
        // Console.WriteLine(RangeExtraction.Extract(new int[]{-3, -2, -1, 2, 10, 15, 16, 18, 19, 20}));
        Console.WriteLine(RangeExtraction.Extract(new int[]{-6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20}));
    }
}