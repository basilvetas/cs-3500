using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExpressions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string sample_sentence = "Find the 4 numbers in this string including 25 and 87 twice (a87test)";

            Regex r_ints   = new Regex("\d+");   // Match integers  (must add @ to front of string to fix error) --> @"\d+"
            Regex r_parens = new Regex("(");   // Match parens  (must add \ in front of '(' --> "\(" and then @ in front of string --> @"\("  )
            Match m = r_ints.Match(sample_sentence);

            Console.Write("All Numbers in String:\n");
            while (m.Success)
            {
                Console.WriteLine(m.Value);
                m = m.NextMatch();
            }

            Console.Write("\nParenthesis in String occur at:\n");

            m = r_parens.Match( sample_sentence );
            while (m.Success)
            {
                Console.WriteLine(m.Index);
                m = m.NextMatch();
            }

            Console.Write("\nMatch Spreadsheet Columns/Rows:\n");


            // Match spreadsheet cell names, extract column and row separately
            // Note the use of parentheses to capture pieces of a match
            r_ints = new Regex(@"([a-zA-Z]+)([1-9]\d*)");
            m = r_ints.Match("AA32 hello BC19AX22the end");
            while (m.Success)
            {
                Console.WriteLine("Row: " + m.Groups[1]);
                Console.WriteLine("Col: " + m.Groups[2]);
                m = m.NextMatch();
            }
            
            // Wait for termination
            Console.ReadLine();
        }
    }
}
