using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Calculator : ICalculator
    {
        public double FirstOperand { get; set; } = 0;
        public double SecondOperand { get; set; } = 0;
        public double Result { get; set; } = 0;

        public double Add()
        {
            Result = FirstOperand + SecondOperand;
            return Result;
        }

        public double Subtract()
        {
            Result = FirstOperand - SecondOperand;
            return Result;
        }
        public double Multiply()
        {
            Result = FirstOperand * SecondOperand;
            return Result;
        }
        public double Divide()
        {
            Result = FirstOperand / SecondOperand;
            return Result;
        }
    }
}
