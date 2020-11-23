using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Test
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator testee;
        public CalculatorTests()
        {
            this.testee = new Calculator();

        }

        [TestMethod]
        public void GetResult_Addition()
        {
            // Arange
            this.testee.FirstOperand.Value = 30;
            this.testee.SecondOperand.Value = 5;
            var expectedResult = 35;
            
            // Act
            var result = this.testee.Add();

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

    }

}
