using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Calculator : ICalculator
    {
        public double Add(double firstOperand, double secondOperand)
        {
            double result = firstOperand + secondOperand;
            return result;
        }
        public double Subtract(double firstOperand, double secondOperand)
        {
            double result = firstOperand - secondOperand;
            return result;
        }
        public double Multiply(double firstOperand, double secondOperand)
        {
            double result = firstOperand * secondOperand;
            return result;
        }
        public double Divide(double firstOperand, double secondOperand)
        {
            double result = firstOperand / secondOperand;
            return result;
        }

    }
}
