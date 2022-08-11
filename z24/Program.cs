using System;
using System.Linq;
using System.Collections.Generic;

// get minimal distance of the graph with range n * n  from the initial position [0, 0] to the final position [N - 1, N - 1]
// Task Path Finder #3: the Alpinist was solved, but did not pass the time check
// You are at the starting point [0, 0] in the mountainous terrain NxN, and you can only move in one of the four main directions
// (i.e. on North, East, South, West). Return the minimum number of climb rounds to the target location [N-1, N-1].
// The number of climb cycles between adjacent locations is defined as the difference in the heights of the location (ascending or descending).
// The height of the location is defined as an integer (0-9).

namespace z24
{
    public class Finder
    {
        public static int PathFinder(string maze)
        {
            DateTime start = DateTime.Now;
            int result = DjikstraAlgorithm(maze.Split('\n').Select(x => x.Select(y => y - 48).ToArray()).ToArray());
            Console.WriteLine($"Execute time for maze: {DateTime.Now - start}");            
            return result;
        }

        private static int DjikstraAlgorithm(int[][] area)
        {
            double[][] distances = Enumerable.Range(0, area.Length).Select(x => Enumerable.Range(0, area.Length).Select(y => double.PositiveInfinity).ToArray()).ToArray();
            PriorityQueue<(int, int, int), int> heap = new PriorityQueue<(int, int, int), int>();
            heap.Enqueue((0, 0, 0), 0);
            List<int> result = new List<int>();
            while (heap.Count > 0)
            {
                var (prevDistance, x, y) = heap.Dequeue();
                if (x == area.Length - 1 && y == area.Length - 1) return result.Min();
                foreach (var (offsetX, offsetY) in new (int, int)[]{(1, 0), (0, -1), (-1, 0), (0, 1)})
                {
                    int newX = x + offsetX;
                    int newY = y + offsetY;
                    if (Enumerable.Range(0, area.Length).Contains(newX) && Enumerable.Range(0, area.Length).Contains(newY))
                    {
                        int newDistance = prevDistance + Math.Abs(area[x][y] - area[newX][newY]);
                        if (prevDistance < distances[newX][newY])
                        {
                            distances[newX][newY] = newDistance;
                            heap.Enqueue((newDistance, newX, newY), newDistance);
                        }
                        if (newX == area.Length - 1 && newY == area.Length - 1) result.Add(newDistance);
                    }
                }
            }
            return -1;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime start = DateTime.Now;
            string a = "000\n000\n000";
            string b = "010\n010\n010";
            string c = "010\n101\n010";
            string d = "0707\n7070\n0707\n7070";
            string e = "700000\n077770\n077770\n077770\n077770\n000007";
            string f = "777000\n007000\n007000\n007000\n007000\n007777";
            string g = "000000\n000000\n000000\n000010\n000109\n001010";
            string h = "9589747\n9141279\n2915307\n5271413\n8795390\n1251866\n4697825";
            string test = string.Join('\n', Enumerable.Range(0, 900).Select(x => string.Join("", Enumerable.Range(0, 900).Select(x => "0"))));

            int resultA = Finder.PathFinder(a);
            int resultB = Finder.PathFinder(b);
            int resultC = Finder.PathFinder(c);
            int resultD = Finder.PathFinder(d); 
            int resultE = Finder.PathFinder(e);
            int resultF = Finder.PathFinder(f);
            int resultG = Finder.PathFinder(g);
            int resultH = Finder.PathFinder(h);
            int resultTest = Finder.PathFinder(test);

            Console.WriteLine($"resultA is {resultA}, {resultA == 0}");
            Console.WriteLine($"resultB is {resultB}, {resultB == 2}");
            Console.WriteLine($"resultC is {resultC}, {resultC == 4}");
            Console.WriteLine($"resultD is {resultD}, {resultD == 42}");
            Console.WriteLine($"resultE is {resultE}, {resultE == 14}");
            Console.WriteLine($"resultF is {resultF}, {resultF == 0}");
            Console.WriteLine($"resultG is {resultG}, {resultG == 4}");
            Console.WriteLine($"resultH is {resultH}, {resultH == 26}");
            Console.WriteLine($"resultTest is {resultTest}, {resultTest == 0}");
            Console.WriteLine($"Execute time for all maze: {DateTime.Now - start}");
            
        }
    }
}
