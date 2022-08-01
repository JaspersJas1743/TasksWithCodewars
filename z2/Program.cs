using System.Collections;
using System.Collections.Generic;
using System.Linq;

/*
In this kata you will create a function that takes a list of non-negative integers and strings and returns a new list with the strings filtered out.

Example
ListFilterer.GetIntegersFromList(new List<object>(){1, 2, "a", "b"}) => {1, 2}
ListFilterer.GetIntegersFromList(new List<object>(){1, 2, "a", "b", 0, 15}) => {1, 2, 0, 15}
ListFilterer.GetIntegersFromList(new List<object>(){1, 2, "a", "b", "aasf", "1", "123", 231}) => {1, 2, 231}
*/

public class ListFilterer
{
    public static IEnumerable<int> GetIntegersFromList(List<object> listOfItems)
    {
        return listOfItems.Where(item => item is int).Select(number => Convert.ToInt32(number));
        // return listOfItems.OfType<int>();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        foreach (var elem in ListFilterer.GetIntegersFromList(new List<object>(){1, 2, "a", "b"}))
            Console.WriteLine(elem);
    }
}