using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/*
Complete the solution so that it strips all text that follows any of a set of comment markers passed in.
Any whitespace at the end of the line should also be stripped out.

Example: 
Given an input string of:
    apples, pears # and bananas
    grapes
    bananas !apples

The output expected would be:
    apples, pears
    grapes
    bananas
The code would be called like so:

string stripped = StripComments Solution.StripComments("apples, pears # and bananas\ngrapes\bananas !apples", new [] { "#", "!" })
// // result should == "apples, pears\ngrapes\nbananas"
*/

public class StripCommentsSolution
{
    public static string StripComments(string text, string[] commentSymbols) => string.Join(
        '\n',
        text.Split('\n')
            .Select(x => x.TrimEnd().Split(
                commentSymbols,
                StringSplitOptions.TrimEntries)[0]
            )
        );
}

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(StripCommentsSolution.StripComments("apples, pears # and bananas\ngrapes\nbananas !apples", new string[]{"#", "!"}));
    }
}