using System;
using System.Linq;

/*
Write a function that takes an integer as input, and returns the number of bits that are equal to one in the binary
representation of that number. You can guarantee that input is non-negative.

Example: The binary representation of 1234 is 10011010010, so the function should return 5 in this case
*/

class Program
{
    public static int countBits(int number) => Convert.ToString(number, 2).Where(x => x == '1').Count();
    
    public static void Main(string[] args)
    {
        Console.WriteLine(countBits(0));
        Console.WriteLine(countBits(1));
        Console.WriteLine(countBits(4));
        Console.WriteLine(countBits(7));
        Console.WriteLine(countBits(1234));
    }
}