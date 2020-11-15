using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Operand
    {
        public double Value { get; set; } = 0;
        public bool HasAPoint { get; set; } = false;

        public void DivideBy100()
        {
            Value /= 100;
        }

        public void Invert()
        {
            Value *= -1;
        }

        public void Reset()
        {
            Value = 0;
            HasAPoint = false;
        }
    }

    public class Calculator : ICalculator
    {
        public Calculator()
        {
            FirstOperand = new Operand();
            SecondOperand = new Operand();
            Result = new Operand();
        }


        public Operand FirstOperand { get; set; }
        public Operand SecondOperand { get; set; }
        public Operand Result { get; set; }



        public double Add()
        {
            Result.Value = FirstOperand.Value + SecondOperand.Value;
            return Result.Value;
        }

        public double Subtract()
        {
            Result.Value = FirstOperand.Value - SecondOperand.Value;
            return Result.Value;
        }
        public double Multiply()
        {
            Result.Value = FirstOperand.Value * SecondOperand.Value;
            return Result.Value;
        }
        public double Divide()
        {
            Result.Value = FirstOperand.Value / SecondOperand.Value;
            return Result.Value;
        }
    }
}
