using System;
using System.Linq;
using System.Collections.Generic;

#region Exercise
/*
You are given a binary tree:
public class Node
{
    public Node Left;
    public Node Right;
    public int Value;
    
    public Node(Node l, Node r, int v)
    {
        Left = l;
        Right = r;
        Value = v;
    }
}
Your task is to return the list with elements from tree sorted by levels,
which means the root element goes first, then root children
(from left to right) are second and third, and so on.
Return empty list if root is 'null'.
Example 1 - following tree:
                 2
            8        9
          1  3     4   5
Should return following list:
[2,8,9,1,3,4,5]
*/
#endregion

namespace z28
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public int Value;
        
        public Node(Node l, Node r, int v)
        {
            Left = l;
            Right = r;
            Value = v;
        }
    }

    class Kata
    {
        public static List<int> TreeByLevels(Node node)
        {
            Queue<Node> queue = new Queue<Node>();
            if (node != null) queue.Enqueue(node);
            List<int> result = new List<int>();
            
            while (queue.Count != 0)
            {
                Node current = queue.Dequeue();

                result.Add(current.Value);

                if (current.Left is not null) queue.Enqueue(current.Left);
                if (current.Right is not null) queue.Enqueue(current.Right);
            }

            return result;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Node n = new Node(
                new Node(
                    new Node(
                        new Node(null, null, 8),
                        new Node(null, null, 9),
                        4),
                    new Node(
                        new Node(null, null, 10),
                        new Node(null, null, 11),
                        5),
                    2), 
                new Node(
                    new Node(
                        new Node(null, null, 12),
                        new Node(null, null, 13),
                        6),
                    new Node(
                        new Node(null, null, 14),
                        new Node(null,null, 15),
                        7),
                    3),
                1
            );
            Node n2 = new Node(
                new Node(
                    new Node(null, null, 4),
                    new Node(new Node(null, null, 8), new Node(null, null, 9), 5),
                    2), 
                new Node(
                    new Node(null, null, 6),
                    new Node(null,null, 7),
                    3),
                1
            );
            var res = Kata.TreeByLevels(n);
            var res2 = Kata.TreeByLevels(n2);
            var expected = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            var expected2 = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8, 9};
            Console.WriteLine(res.SequenceEqual(expected));
            Console.WriteLine(res2.SequenceEqual(expected2));
        }
    }
}