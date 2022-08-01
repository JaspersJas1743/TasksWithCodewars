using System;
using System.Linq;

/*
Simple, given a string of words, return the length of the shortest word(s).

String will never be empty and you do not need to account for different data types.
*/
public class Kata
{
    public static int FindShort(string s) => s.Split(" ").Select(word => word.Length).Min(); 
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Kata.FindShort("bitcoin take over the world maybe who knows perhaps"));
    }
}