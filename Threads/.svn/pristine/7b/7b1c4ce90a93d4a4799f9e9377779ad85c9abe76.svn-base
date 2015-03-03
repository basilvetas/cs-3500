using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LectureExamples
{
    /// <summary>
    /// Examples of using delegates
    /// </summary>
    public class Delegates
    {
        /// <summary>
        /// Some example code
        /// </summary>
        public static void Main(string[] args)
        {
            // Create a list with three elements
            List<String> list = new List<String>();
            list.Add("hello");
            list.Add("long string");
            list.Add("a");

            // Search the list for a string that is longer than 5 characters.  Note that we pass
            // in a method (LongerThan5) as the delegate parameter.
            Console.WriteLine(SearchList(list, LongerThan5));

            // Search the list for a string that is exactly 5 characters along.  We create an
            // anonymous method to pass as the delegate parameter.
            Console.WriteLine(SearchList(list, (s) => s.Length == 5));

            // Methods can be stored in variables (and passed as parameters and returned as
            // results.  Here are two different ways for expressing delegate types.
            Finder f1 = LongerThan5;
            Func<String, bool> f2 = LongerThan5;
            Console.WriteLine(f1("joe"));
            Console.WriteLine(f2("joe"));

            // Here we create an anonymous method and store it (into two different variables)
            f1 = (s) => { bool x = s.Length == 3; return x; };
            f2 = (s) => { bool x = s.Length == 3; return x; };
            Console.WriteLine(f1("joe"));
            Console.WriteLine(f1("joe"));

            // Print everything in the list
            ApplyToAll(list, Console.WriteLine);

            Console.ReadLine();
        }

        /// <summary>
        /// This declares that something of type "Finder" is a method that takes a String as its
        /// parameter and returns a bool as its result.
        /// </summary>
        public delegate bool Finder(String s);

        /// <summary>
        /// A SearchList method that uses the Finder delegate to type its second parameter.
        /// </summary>
        public static String SearchList(List<String> list, Finder f)
        {
            foreach (String s in list)
            {
                if (f(s))
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>
        /// A SearchList method that uses a parameterized Func to type its second parameter.
        /// </summary>
        public static String SearchListPrime(List<String> list, Func<String, bool> f)
        {
            foreach (String s in list)
            {
                if (f(s))
                {
                    return s;
                }
            }
            return null;
        }


        /// <summary>
        /// A method that uses a void method as its second parameter.
        /// </summary>
        public static void ApplyToAll(List<String> list, Action<String> proc)
        {
            foreach (String s in list)
            {
                proc(s);
            }
        }


        /// <summary>
        /// Used in the examples above
        /// </summary>
        public static bool LongerThan5(String s)
        {
            return s.Length > 5;
        }
    }
}
