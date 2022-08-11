using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

#region Exercise
// Create an endless stream of prime numbers - a bit like IntStream.of(2, 3, 5, 7, 11, 13, 17), but infinite.
// The stream must be able to produce a million primes in a few seconds
// In the solution, I used an algorithm called "Optimized Sundaram Sieve",
// the stated search time for primes is up to a billion: 6.5 seconds.
#endregion

namespace z25
{
    public class Primes
    {
        public static IEnumerable<int> Stream()
        {
            int length = 60000000;
            var numbers = new BitArray((length + 1) / 2);

            for (int i = 1; i + i + 2 * i * i < numbers.Length; i++)
                if (!numbers[i])
                    for (int j = i; i + j + 2 * i * j < numbers.Length; j++)
                        numbers[i + j + 2 * i * j] = true;

            yield return 2;
            for (int i = 1; i < numbers.Length; i++)
                if (!numbers[i])
                    yield return (i * 2 + 1);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var elem in Primes.Stream().Skip(1000).Take(10))
                Console.WriteLine(elem);
        }
    }
}