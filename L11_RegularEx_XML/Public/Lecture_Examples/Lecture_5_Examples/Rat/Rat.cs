using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lecture_Examples
{

    public class RatDemo
    {

        public static void Main(String[] args)
        {
            Rat r1 = new Rat(3, 4);
            Rat r2 = new Rat(5, 6);
            Console.WriteLine(r1 + r2);

            Console.ReadLine();
        }

    }


    /// <summary>
    /// Provides rational numbers that can be expressed as ratios
    /// of 32-bit integers.  Rats are immutable.
    /// </summary>
    public class Rat
    {
        // Abstraction function: 
        // A Rat represents the rational num/den

        // Representation invariant:
        //  den > 0
        //  gcd(|num|, den) = 1
        private int num;
        private int den;


        /// <summary>
        /// Creates 0
        /// </summary>
        public Rat()
            : this(0, 1)      // This invokes the 2-argument constructor
        {
        }

        /// <summary>
        /// Creates n
        /// </summary>
        /// <param name="n">Numerator, where denominator is 1</param>
        public Rat(int n)
            : this(n, 1)      // This invokes the 2-argument constructor
        {
        }

        /// <summary>
        /// Creates n/d.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if d == 0</exception>
        /// <param name="n">Numerator</param>
        /// <param name="d">Denominator, which must not be zero</param>
        public Rat(int n, int d)
        {
            if (d == 0)
            {
                throw new ArgumentException("Zero denominator not allowed");
            }
            int g = n.Gcd(d);
            if (d > 0)
            {
                num = n / g;
                den = d / g;
            }
            else
            {
                num = -n / g;
                den = -d / g;
            }
        }


        /// <summary>
        /// Returns the sum of r1 and r2.
        /// </summary>
        /// <exception cref="System.OverflowException">When arithmetic overflow</exception>
        /// <param name="r1">Addend</param>
        /// <param name="r2">Addend</param>
        /// <returns></returns>
        public static Rat operator +(Rat r1, Rat r2)
        {
            checked
            {
                return new Rat(r1.num * r2.den + r1.den * r2.num, 
                               r1.den * r2.den);
            }
        }


        /// <summary>
        /// Returns a standard string representation of a rational number
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (den == 1)
            {
                return num.ToString();
            }
            else
            {
                return num + "/" + den;
            }
        }


        /// <summary>
        /// Reports whether this and o are the same rational number.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals(object o)
        {
            // Cast o to be a Rat.  If the cast fails, we get null back.
            Rat r = o as Rat;

            // Make sure r is non-null and its numerator and denominator
            // the same as those of this.
            return
                !ReferenceEquals(r, null) &&
                this.num == r.num &&
                this.den == r.den;
        }


        /// <summary>
        /// Tests for equality. Notice double null test!
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator ==(Rat r1, Rat r2)
        {
            if (ReferenceEquals(r1, null)) {
                return ReferenceEquals(r2, null);
            }
            else
            {
                return r1.Equals(r2);
            }
        }


        /// <summary>
        /// Casts an int to a Rat
        /// </summary>
        /// <param name="n">Int to be cast</param>
        /// <returns></returns>
        public static implicit operator Rat(int n)
        {
            return new Rat(n);
        }



        /// <summary>
        /// Tests for inequality
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public static bool operator !=(Rat r1, Rat r2)
        {
            return !(r1 == r2);
        }


        /// <summary>
        /// Returns a hash code for this Rat.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return num ^ den;
        }

    }

}

