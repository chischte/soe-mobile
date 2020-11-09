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
            // Arrange
            testee.FirstOperand.Value = 1;
            testee.SecondOperand.Value = 1;
            
            // Act
            testee.Add();
            var result = this.testee.Result.Value;

            // Assert
            Assert.AreEqual(2, result);
        } 
        
        [TestMethod]
        public void GetResults_One_Minus_One_ReturnsCorrectResult()
        {
            // Arrange
            testee.FirstOperand.Value = 1;
            testee.SecondOperand.Value = 1;
            
            // Act
            testee.Subtract();
            var result = this.testee.Result.Value;

            // Assert
            Assert.AreEqual(0, result);
        }
        
        [TestMethod]
        public void GetResults_Multiply_ReturnsCorrectResult()
        {
            // Arrange
            testee.FirstOperand.Value = 12;
            testee.SecondOperand.Value = 12;
            
            // Act
            testee.Multiply();
            var result = this.testee.Result.Value;

            // Assert
            Assert.AreEqual(144, result);
        }  
        
        [TestMethod]
        public void GetResults_Divide_ReturnsCorrectResult()
        {
            // Arrange
            testee.FirstOperand.Value = 144;
            testee.SecondOperand.Value = 12;
            
            // Act
            testee.Divide();
            var result = this.testee.Result.Value;

            // Assert
            Assert.AreEqual(12, result);
        }
        
        [TestMethod]
        public void GetResults_Percentage_First_Operand_ReturnsCorrectResult()
        {
            // Arrange
            testee.FirstOperand.Value = 3660;
            
            // Act
            testee.FirstOperand.DivideBy100();;
            var result = this.testee.FirstOperand.Value;

            // Assert
            Assert.AreEqual(36.6, result);
        }  
        
        [TestMethod]
        public void GetResults_Invert_Second_Operand_ReturnsCorrectResult()
        {
            // Arrange
            testee.SecondOperand.Value = -3660;
            
            // Act
            testee.SecondOperand.Invert();
            var result = this.testee.SecondOperand.Value;

            // Assert
            Assert.AreEqual(3660, result);
        }    
        
        [TestMethod]
        public void Check_If_Operand_Reset_Works()
        {
            // Arrange
            testee.Result.Value = -3660;
            testee.Result.HasAPoint = true;
            
            // Act
            testee.Result.Reset();
            var resultValue = this.testee.Result.Value;
            var resultHasAPoint = this.testee.Result.HasAPoint;


            // Assert
            Assert.AreEqual(0, resultValue);
            Assert.AreEqual(false, resultHasAPoint);
        }
    }
}
