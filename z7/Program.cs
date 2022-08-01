using System;
using System.Linq;

/*
Implement the function unique_in_order which takes as argument a sequence and returns a list of items without 
any elements with the same value next to each other and preserving the original order of elements.

For example:
uniqueInOrder("AAAABBBCCDAABBB") == {'A', 'B', 'C', 'D', 'A', 'B'}
uniqueInOrder("ABBCcAD")         == {'A', 'B', 'C', 'c', 'A', 'D'}
uniqueInOrder([1,2,2,3,3])       == {1,2,3}
*/

public static class Kata
{
    public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable) => (iterable.Count() == 0) ? Enumerable.Empty<T>()
                                                                                : iterable.SkipLast(1)
                                                                                .Where((x, y) => !x.Equals(iterable.ElementAt(y + 1)))
                                                                                .Append(iterable.Last());
}
class Program
{
    public static void Main(string[] args)
    {
        foreach (var elem in Kata.UniqueInOrder("AAAABBBCCDAABBB"))
            Console.Write(elem);
        Console.WriteLine();
        foreach (var elem in Kata.UniqueInOrder("ABBCcAD"))
            Console.Write(elem);
        Console.WriteLine();
        foreach (var elem in Kata.UniqueInOrder(new int[]{1, 2, 2, 3, 3}))
            Console.Write(elem);
        Console.WriteLine();
        Console.WriteLine("Last: ");
        Console.Write(Kata.UniqueInOrder(""));
        
    }
}