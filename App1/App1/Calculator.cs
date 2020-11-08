using System;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace App1
{

    public class Operand
    {
        public double Value { get; set; } = 0;
        public bool HasAPoint { get; set; } = false;

        public void divideBy100()
        {
            Value /= 100;
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
        }
        
        
        public Operand FirstOperand { get; set; }
        public Operand SecondOperand { get; set; }
        public double Result { get; set; } = 0;



       public double Add()
        {
            Result = FirstOperand.Value + SecondOperand.Value;
            return Result;
        }

        public double Subtract()
        {
            Result = FirstOperand.Value - SecondOperand.Value;
            return Result;
        }
        public double Multiply()
        {
            Result = FirstOperand.Value * SecondOperand.Value;
            return Result;
        }
        public double Divide()
        {
            Result = FirstOperand.Value / SecondOperand.Value;
            return Result;
        }
    }
}
