using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture_Examples
{
    public static class Extensions
    {
        static void Main(string[] args)
        {
            string s = "The TAs are great!";
            Console.WriteLine(s.ToUpper());
            Console.WriteLine(EveryOtherUpper( s ));

            //
            //  Wouldn't it be nice to use the following syntax:
            //
            //  Console.WriteLine(s.EveryOtherUpper( ));

            Console.Read();
        }
        /// <summary>
        ///   Show how to convert a function that takes an object into an extension
        ///   that allows the object.function() notation.
        /// </summary>
        /// <param name="s"> The string to change capitalization of </param>
        /// <returns> A StRiNg CaPiTaLiZeD</returns>
        private static string EveryOtherUpper(String s)
        {
            string result = "";
            bool upper = true;

            foreach (char c in s)
            {
                char value = c;
                if (upper) { value = value.ToString().ToUpper()[0]; }

                result += value;

                upper = !upper;
            }

            return result;
        }

        
        // Extension methods must be defined as static methods inside static
        // classes.  The first parameter of the method is prefixed with the
        // keyword this.  

        /// <summary>
        /// Returns the GCD of a and b. 
        /// </summary>
        public static int Gcd(this int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b > 0)
            {
                int temp = a % b;
                a = b;
                b = temp;
            }
            return a;
        }


    }
}
