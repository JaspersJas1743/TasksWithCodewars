using System;
using System.Linq;
using System.Numerics;
using System.Collections.Generic;

/*
We need to sum big numbers and we require your help.

Write a function that returns the sum of two numbers. The input numbers are strings and the function must return a string.

Example
add("123", "321"); -> "444"
add("11", "99");   -> "110"
Notes
The input numbers are big.
The input is a string of only digits
The numbers are positives
*/

public class Kata
{
    public static string? Add(string a, string b) => Convert.ToString(BigInteger.Parse(a) + BigInteger.Parse(b));
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Kata.Add("12345654312456315246", "21325125326236424121"));
    }
}