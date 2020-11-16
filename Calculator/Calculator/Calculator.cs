using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{


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

        public Operand GetCurrentOperandObject()
        {
            return FirstOperand;
        }

        private OperationMode operationMode = OperationMode.Add;

        public void SetOperationMode(OperationMode operationMode)
        {
            this.operationMode = operationMode;
        }

        public void CalculateResult()
        {
            switch (operationMode)
            {
                case OperationMode.Add:
                    {
                        Add();
                        break;
                    }
                case OperationMode.Subtract:
                    {
                        Subtract();
                        break;
                    }

                case OperationMode.Multiply:
                    {
                        Multiply();
                        break;
                    }

                case OperationMode.Divide:
                    {
                        Divide();
                        break;
                    }
            }
        }


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
