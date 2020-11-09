using System;
using System.Collections.Generic;
using System.Text;
using App1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest
{
    [TestClass]
    public class CalculatorTests
    {
        private readonly Calculator testee;

        public CalculatorTests()
        {
            this.testee = new Calculator();
        }

        [TestMethod]
        public void GetResults_One_Plus_One_ReturnsCorrectResult()
        {
            // Arange
            testee.FirstOperand.Value = 1;
            testee.SecondOperand.Value = 1;
            
            // Act
            testee.Add();
            
            var result = this.testee.Result.Value;

            // Assert
            Assert.AreEqual(2, result);
        }
        //[TestMethod]
        //public void GetResults_Five_Minus_Three_returnsCorrectResult()
        //{
        //    // Act
        //    var result = this.testee.Subtract(5, 3);

        //    // Assert
        //    Assert.AreEqual(2, result);
        //}

        //[TestMethod]
        //public void GetResults_Five_Times_Six_returnsCorrectResult()
        //{
        //    // Act
        //    var result = this.testee.Multiply(5, 6);

        //    // Assert
        //    Assert.AreEqual(30, result);
        //}

        //[TestMethod]
        //public void GetResults_30_diveded_by_6_returnsCorrectResult()
        //{
        //    // Act
        //    var result = this.testee.Divide(30, 6);

        //    // Assert
        //    Assert.AreEqual(5, result);
        //}


    }
}
