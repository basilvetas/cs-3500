using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Evaluator_Example
{
    class Program
    {
        public static int all_values_are_5(String s)
        {
            return 5;
        }

        static void Main(string[] args)
        {

            int result = Evaluator_Example.Eval.Evaluate("abc", all_values_are_5);

            if (result == 5)
            {
                Console.WriteLine("yes! it worked!");
            }
            else
            {
                Console.WriteLine("oops. error in evaluator");

            }
            Console.ReadLine();
        }

        //public static int value;

        //public static int all_values_are_5(String s)
        //{
        //    return value;
        //}

        //static void Main(string[] args)
        //{
        //    value = 100;

        //    int result = Evaluator_Example.Eval.Evaluate("abc", all_values_are_5);

        //    if (result == value)
        //    {
        //        Console.WriteLine("yes! it worked!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("oops. error in evaluator");

        //    }
        //    Console.ReadLine();
        //}
    }
}
