using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Test
{
    [TestClass]
    public class MainPageViewModelTests
    {
        private readonly Mock<ICalculator> calculatorMock;
        private readonly MainPageViewModel testee;

        public MainPageViewModelTests()
        {
            this.calculatorMock = new Mock<ICalculator>(MockBehavior.Loose);
            this.testee = new MainPageViewModel(calculatorMock.Object);
        }

        [TestMethod]
        public void SetOperationCommand_CallsCorrespondingMethod()
        {
            // Arrange
            this.testee.AddNumberCommand.Execute("1");
            this.testee.SetOperationCommand.Execute("+");
            this.testee.AddNumberCommand.Execute("1");

            // Act
            this.testee.SetOperationCommand.Execute("=");
            

            // Assert
            this.calculatorMock.Verify(m => m.CalculateResult(), Times.Once);
           
        }

    }

}
