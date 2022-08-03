using System;
using System.Linq;
using System.Collections.Generic;

/*
A Hamming number is a positive integer of the form 2i3j5k, for some non-negative integers i, j, and k.

Write a function that computes the nth smallest Hamming number.

Specifically:
The first smallest Hamming number is 1 = 2^0*3^0*5^0
The second smallest Hamming number is 2 = 2^1*3^0*5^0
The third smallest Hamming number is 3 = 2^0*3^1*5^0
The fourth smallest Hamming number is 4 = 2^2*3^0*5^0
The fifth smallest Hamming number is 5 = 2^0*3^0*5^1

The 20 smallest Hamming numbers are given in the Example test fixture.
*/

public class Hamming
{
    private static List<long> _hammingNumber = new List<long>(){1};

    private static int exp2 = 0, exp3 = 0, exp5 = 0;

    public static long hamming(int n) {
        if (_hammingNumber.Count >= n) return _hammingNumber.ElementAt(n - 1);
    
        while (_hammingNumber.Count < n) {
            long new2 = _hammingNumber.ElementAt(exp2) * 2,
                new3 = _hammingNumber.ElementAt(exp3) * 3,
                new5 = _hammingNumber.ElementAt(exp5) * 5,
                temp = Math.Min(Math.Min(new2, new3), new5);

            if (temp == new2) ++exp2;
            if (temp == new3) ++exp3;
            if (temp == new5) ++exp5;

            _hammingNumber.Add(temp);
        }

        return _hammingNumber.Last();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        DateTime startScript = DateTime.Now;
        foreach (var elem in Enumerable.Range(1, 20)) {
            DateTime start = DateTime.Now;        
            Console.WriteLine($"haming({elem}) is {Hamming.hamming(elem)}. Time spent script: {DateTime.Now - start}");
        }
        Console.WriteLine($"Time spent script: {DateTime.Now - startScript}");
    }
}