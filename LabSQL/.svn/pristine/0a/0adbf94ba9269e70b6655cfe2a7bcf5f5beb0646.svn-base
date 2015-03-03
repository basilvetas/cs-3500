using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enumerable
{
    /// <summary>
    /// Motivates iteration abstraction
    /// </summary>
    public class Hailstone
    {
        public static void Main (String[] args) {
            /*
            // Print out sequence starting at 7
            long n = 7;
            Console.WriteLine(n);
            while (n > 1)
            {
                n = NextHailstone(n);
                Console.WriteLine(n);
            }
            Console.WriteLine();
            Console.ReadLine();
             */

            // Use a method to do the printing
            foreach (int n in Hailstone4(7))
            {
                Console.WriteLine(n);
            }
            Console.WriteLine();
            Console.ReadLine();

            // Compute the sequence, then print it
            foreach (long m in Hailstone2(7)) {
                Console.WriteLine(m);
            }
            Console.WriteLine();
            Console.ReadLine();

            // Find the largest element in the sequence
            long largest = 0;
            foreach (long m in Hailstone2(7))
            {
                largest = Math.Max(largest, m);
            }
            Console.WriteLine("Largest: " + largest);
            Console.ReadLine();

            // Is there an element that is a multiple of 5?
            Console.WriteLine("Multiple: " + FindMultiple(Hailstone2(7), 5));
            Console.ReadLine();

        }


        /// <summary>
        /// Returns the first element from the sequence that is a multiple of 5.  If there are no
        /// such multiples, returns -1.
        /// </summary>
        /// <param name="seq"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static long FindMultiple(IEnumerable<long> seq, long d)
        {
            foreach (long n in seq)
            {
                if (n % d == 0)
                {
                    return n;
                }
            }
            return -1;
        }


        /// <summary>
        /// Returns the hailstone number that follows n.
        /// </summary>
        public static long NextHailstone(long n)
        {
            if (n == 1)
            {
                return 1;
            }
            else if (n % 2 == 0)
            {
                return n / 2;
            }
            else
            {
                return 3 * n + 1;
            }
        }


        /// <summary>
        /// Prints out the hailstone sequence beginning at n.
        /// </summary>
        public static void Hailstone1(long n)
        {
            Console.WriteLine(n);
            while (n > 1)
            {
                n = NextHailstone(n);
                Console.WriteLine(n);
            }
        }


        /// <summary>
        /// Returns a list containing the hailstone sequence beginning at n.
        /// </summary>
        public static List<long> Hailstone2(long n)
        {
            List<long> result = new List<long>();
            result.Add(n);
            while (n > 1)
            {
                n = NextHailstone(n);
                result.Add(n);
            }
            return result;
        }


        /// <summary>
        /// Enumerates the hailstone sequence beginning at n.
        /// </summary>
        public static IEnumerable<long> Hailstone3(long n)
        {
            List<long> result = new List<long>();
            result.Add(n);
            while (n > 1)
            {
                n = NextHailstone(n);
                result.Add(n);
            }
            return result;
        }


        /// <summary>
        /// Enumerates the hailstone sequence beginning at n.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<long> Hailstone4(long n)
        {
            yield return n;
            while (n > 1)
            {
                n = NextHailstone(n);
                yield return n;
            }
        }

    }

}
