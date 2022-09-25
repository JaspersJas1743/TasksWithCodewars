using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

#region Exercise
/*
Task
A rectangle with sides equal to even integers a and b is drawn on the Cartesian plane.
Its center (the intersection point of its diagonals) coincides with the point (0, 0),
but the sides of the rectangle are not parallel to the axes; instead, they are forming
45 degree angles with the axes. How many points with integer coordinates are located
inside the given rectangle (including on its sides)?
Example
For a = 6 and b = 4, the output should be 23
The following picture illustrates the example, and the 23 points are marked green.
Input/Output
[input] integer a
A positive even integer.
Constraints: 2 ≤ a ≤ 10000.
[input] integer b
A positive even integer.
Constraints: 2 ≤ b ≤ 10000.
[output] an integer
The number of inner points with integer coordinates.
*/
#endregion

namespace myjinxin
{
    public class Kata
    {
        public static int RectangleRotation(int a, int b)
        {
            int biggest = Math.Max(a, b), smallest = Math.Min(a, b);
            double halfWidth = Math.Floor(Math.Sqrt(Math.Pow(biggest, 2) / 8)),
                halfHeight = Math.Floor(Math.Sqrt(Math.Pow(smallest, 2) / 8)),
                widthOfFirstRectangle = 2 * halfWidth + 1,
                heightofFirstRectangle = 2 * halfHeight + 1,
                widthOfSecondRectangle = widthOfFirstRectangle,
                heightOfSecondRectangle = 2 * (Math.Floor((smallest / 2 - 1 / Math.Sqrt(2)) / Math.Sqrt(2)) + 1);
            if (Math.Sqrt(2 * Math.Pow(halfWidth, 2)) + 1 / Math.Sqrt(2) <= biggest / 2) widthOfSecondRectangle += 1;
            else widthOfSecondRectangle -= 1;
            
            return (int)(widthOfFirstRectangle * heightofFirstRectangle + widthOfSecondRectangle * heightOfSecondRectangle);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(23 == Kata.RectangleRotation(6,4));
            Console.WriteLine(65 == Kata.RectangleRotation(30,2));
            Console.WriteLine(49 == Kata.RectangleRotation(8,6));
            Console.WriteLine(333 == Kata.RectangleRotation(16,20));
        }
    }
}