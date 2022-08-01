using System;

/*
You probably know the "like" system from Facebook and other pages. People can "like" blog posts, pictures or other items. We want to create the text that should be displayed next to such an item.

Implement the function which takes an array containing the names of people that like an item. It must return the display text as shown in the examples:

[]                                -->  "no one likes this"
["Peter"]                         -->  "Peter likes this"
["Jacob", "Alex"]                 -->  "Jacob and Alex like this"
["Max", "John", "Mark"]           -->  "Max, John and Mark like this"
["Alex", "Jacob", "Mark", "Max"]  -->  "Alex, Jacob and 2 others like this"
Note: For 4 or more names, the number in "and 2 others" simply increases.
*/

public static class Kata
{
    public static string Likes(string[] names)
    {
        if (names.Length < 2) return $"{((names.Length == 0) ? "no one" : names[0])} likes this";
        string result = names[0];
        if (names.Length > 2) result += $", {names[1]}";
        result += $" and {((names.Length < 4) ? names.Last() : $"{names.Length - 2} others")} like this";
        return result;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Kata.Likes(new string[]{}));
    }
}