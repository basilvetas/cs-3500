using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormulaEvaluator;

namespace FormulaEvaluatorTester
{
    /// <summary>
    ///     A Console Application for testing the FormulaEvaluator Class Library
    /// </summary>
    public class EvaluatorTester
    {
        /// <summary>
        ///     Main method for testing the FormulaEvaluator class
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Test cases for infix calculator
 
           

            // simple addition
            int result_one = Evaluator.Evaluate(" 1 + 1 ", LookupMethod);
            Console.WriteLine(" = " + result_one);
            Console.Read();

            // simple subtraction
            int result_two = Evaluator.Evaluate(" 6 -  1 ", LookupMethod);
            Console.WriteLine(" = " + result_two);
            Console.Read();

            // simple multiplication
            int result_three = Evaluator.Evaluate(" 4 *  2 ", LookupMethod);
            Console.WriteLine(" = " + result_three);
            Console.Read();

            // simple division
            int result_four = Evaluator.Evaluate(" 9 / 3 ", LookupMethod);
            Console.WriteLine(" = " + result_four);
            Console.Read();

            // complex addition and subtraction            
            int result_five = Evaluator.Evaluate(" (15 + 15) - (12 + 8) ", LookupMethod);
            Console.WriteLine(" = " + result_five);
            Console.Read();

            // complex addition and subtraction
            int result_six = Evaluator.Evaluate(" (10 - 10) + 0 + 6  ", LookupMethod);
            Console.WriteLine(" = " + result_six);
            Console.Read();

            // complex multiplication and division
            try{        
                int result_seven = Evaluator.Evaluate(" (4 * 2) / (4 - 4) ", LookupMethod);
                Console.WriteLine(" = " + result_seven);                
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Error: Divide by 0");
            }
            Console.Read();
           
            // complex multiplication and division
            int result_eight = Evaluator.Evaluate(" (9 / 3) * 10 ", LookupMethod);
            Console.WriteLine(" = " + result_eight);
            Console.Read();

            // simple variable case
            int result_nine = Evaluator.Evaluate(" (10 / D4) ", LookupMethod);
            Console.WriteLine(" = " + result_nine); // A4 should equal 5 so result => 2
            Console.Read();

            // complex variable case
            int result_ten = Evaluator.Evaluate(" (50 / JGKLDSBV39837) * 10 ", LookupMethod);
            Console.WriteLine(" = " + result_ten); // result should equal 100
            Console.Read();

            // improper variable case one
            try
            {
                int result_eleven = Evaluator.Evaluate(" (50 / 9C) ", LookupMethod);
                Console.WriteLine(" = " + result_eleven);
                Console.Read();
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Error: Invalid Variable");
            }
            
            // improper variable case two
            try
            {
                int result_twelve = Evaluator.Evaluate(" (50 / 4   4) ", LookupMethod);
                Console.WriteLine(" = " + result_twelve);
            }            
            catch(ArgumentException e)
            {
                Console.WriteLine("Error: Invalid Variable");
            }
            Console.Read();

            // crazy spaces test
            int result_thirteen = Evaluator.Evaluate(" (   50    /    C9   ) +  4 *         2", LookupMethod);
            Console.WriteLine(" = " + result_thirteen); // should be 18
            Console.Read();

            // example test one
            int result_fourteen = Evaluator.Evaluate("(2 + 3) * 5 + 2", LookupMethod);
            Console.WriteLine(" = " + result_fourteen); // should be 27
            Console.Read();

            // example test two
            int result_fifteen = Evaluator.Evaluate("(2 + X6) * 5 + 2", LookupMethod);
            Console.WriteLine(" = " + result_fifteen); // should be 47
            Console.Read();

            // improper variable case three
            try
            {
                int result_sixteen = Evaluator.Evaluate("(2 + X2345366X23462456) * 5 + 2", LookupMethod);
                Console.WriteLine(" = " + result_sixteen); 
                Console.Read();
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Error: Invalid Variable");
            }

            // improper variable case four
            try
            {
                int result_seventeen = Evaluator.Evaluate("(2 + GGGGGGGG) * 5 + 2", LookupMethod);
                Console.WriteLine(" = " + result_seventeen); 
                Console.Read();
            }         
             catch(ArgumentException e)
            {
                Console.WriteLine("Error: Invalid Variable");
            }

            Console.Read();
        }

        /// <summary>
        ///     A Lookup method to be passed into Evaluate as the delegate method
        /// </summary>
        /// <param name="v">The name of the variable</param>
        /// <returns>The integer value of the variable</returns>
        public static int LookupMethod(string v)
        {
            if (v.Equals("X6"))
                return 7;
            else
                return 5;

        }
    }
}
