using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComplexNumbers;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Complex c = new Complex(2, 3);
            Assert.AreEqual(2, c.Real);
            Assert.AreEqual(3, c.Imag);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Complex c1 = new Complex(2, 3);
            Complex c2 = new Complex(4, 5);
            Complex sum = c1 + c2;
            Assert.AreEqual(6, sum.Real);
            Assert.AreEqual(8, sum.Imag);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Complex c1 = new Complex(2, 3);
            Complex c2 = new Complex(4, 5);
            Complex prod = c1 * c2;
            Assert.AreEqual(-7, prod.Real);
            Assert.AreEqual(22, prod.Imag);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Complex c1 = new Complex(5);
            Assert.AreEqual("5", c1.ToString());
        }


    }
}
