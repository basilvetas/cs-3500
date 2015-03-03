using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture_Examples
{
    /// <summary>
    ///  Lecture 8 Examples - Equals and ==, Objects and defined types.
    ///  Author: H. James de St. Germain
    ///  Date: Fall 2014
    /// </summary>
    class Equality_Playground
    {
        /// <summary>
        /// Just a chance to play with the various equality operations
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ABC abc_1 = new ABC(1);
            Object disquised_string_1 = "happy";
            string string_1 = "happy";
            ABC abc_2 = new ABC(2);
            Object disquised_abc_1 = new ABC(1);
            Object disquised_abc_2 = new ABC(2);
            int count = 0;

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( disquised_string_1.Equals(string_1))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( string_1.Equals(disquised_string_1))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( abc_1.Equals(disquised_string_1))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( abc_1.Equals(string_1))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if (disquised_string_1.Equals(string_1))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if (abc_1 == abc_2)
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( disquised_abc_1 == disquised_abc_2 )
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if ( disquised_abc_1.Equals(disquised_abc_2))
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if (abc_1 == disquised_abc_1)
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            if (disquised_abc_1 == abc_1 )
            {
                Console.WriteLine("yes " + count);
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            ABC abc_3 = 4;
            if (abc_3 == new ABC(4))
            {
                Console.WriteLine("yes the implicit cast worked");
            }

            Console.WriteLine("--- " + count++ + " --------------------------------------------------");
            ABC abc_4 = "hello";
            ABC abc_5 = 10;
            if (abc_4 == abc_5)
            {
                Console.WriteLine("yes the string cast works");
            }



            Console.Read();
        }
    }

    class ABC
    {

        private int value;

        public ABC(int x)
        {
            this.value = x;
        }



        /// <summary>
        /// allows implict conversion between ints and ABCs
        /// </summary>
        /// <param name="n">Int to be cast</param>
        /// <returns></returns>
        public static implicit operator ABC(int n)
        {
            Console.WriteLine("in implicit cast");
            return new ABC(n);
        }

        public override bool Equals( Object other )
        {
            Console.WriteLine("in ABC equals");
            ABC other_abc = other as ABC;

            return !ReferenceEquals(other_abc, null) && this.value == other_abc.value;
        }

        /// <summary>
        /// allows implict conversion between strings and ABCs
        /// </summary>
        /// <param name="n">Int to be cast</param>
        /// <returns></returns>
        public static implicit operator ABC(string n)
        {
            Console.WriteLine("in implicit cast");
            return new ABC(10);
        }

        public static bool operator==( ABC first, ABC second )
        {
            Console.WriteLine("in ABC ==");
            // if (first == null) return (second == null);
            if (ReferenceEquals(first, null)) return (ReferenceEquals(second, null));
            return first.Equals(second);
        }
        /// <summary>
        ///  Error in the below code.  What is it?
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator==( ABC first, Object second )
        {
            Console.WriteLine("in ABC == Object");
            if (first == null) return (second == null);
            return first.Equals(second);
        }

        /// <summary>
        ///  Should we use ! Equals, or ! ==, or just do the check here?
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool operator!=( ABC first, Object second )
        {
            Console.WriteLine("in ABC != Object");
            return ! first.Equals(second);
        }

        public static bool operator!=( ABC first, ABC second )
        {
            Console.WriteLine("in ABC !=");
            return !(first == second);
        }

    }
}
