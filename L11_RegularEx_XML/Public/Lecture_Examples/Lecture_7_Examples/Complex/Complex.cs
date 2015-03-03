// Written by Joe Zachary for CS 3500, Fall 2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexNumbers
{
    /// <summary>
    /// Provides objects that represent complex numbers.  The default
    /// constructor creates the complex number zero.
    /// </summary>
    public struct Complex
    {
        /// <summary>
        /// Constructs number with real part r and no imaginary part
        /// </summary>
        public Complex(double r)
            : this(r, 0)
        {
        }

        /// <summary>
        /// Constructs number with real part r and imaginary part i
        /// </summary>
        /// <param name="r"></param>
        /// <param name="i"></param>
        public Complex(double r, double i)
            : this()
        {
            Real = r;
            Imag = i;
        }

        /// <summary>
        /// The real part of this number
        /// </summary>
        public double Real { get; private set; }

        /// <summary>
        /// The imaginary part of this number
        /// </summary>
        public double Imag { get; private set; }

        /// <summary>
        /// Returns c1+c2
        /// </summary>
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imag + c2.Imag);
        }

        /// <summary>
        /// Returns c1*c2
        /// </summary>
        public static Complex operator *(Complex c1, Complex c2)
        {
            double realPart = c1.Real * c2.Real - c1.Imag * c2.Imag;
            double imagPart = c1.Real * c2.Imag + c1.Imag * c2.Real;
            return new Complex(realPart, imagPart);
        }

        /// <summary>
        /// Returns the standard string form of this number
        /// </summary>
        public override string ToString()
        {
            if (Imag == 0)
            {
                return Real.ToString();
            }
            else if (Real == 0)
            {
                return Imag + "i";
            }
            else if (Imag < 0)
            {
                return Real.ToString() + Imag + "i";
            }
            else
            {
                return Real + "+" + Imag + "i";
            }
        }
    }
}
