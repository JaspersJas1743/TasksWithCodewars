using System;
using System.Linq;
using System.Collections.Generic;

public class Kata
{
    public static string Factorial(int n)
    {
        if (n < 0) return null;
        
        int result = 1;
        for (int i = 2; i <= n; ++i) result *= i;
        
        return result.ToString();
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Kata.Factorial(Convert.ToInt32(Console.ReadLine())));
    }
}