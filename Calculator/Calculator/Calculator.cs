using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

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
            switch (operationStage)
            {
                case OperationStage.EnterFirstOperand:
                    {
                        return FirstOperand;
                    }
                case OperationStage.EnterSecondOperand:
                    {
                        return SecondOperand;
                    }
                case OperationStage.DisplayResult:
                    {
                        return Result;
                    }
                default:
                    {
                        return FirstOperand;
                    }
            }
        }

        private OperationMode operationMode = OperationMode.Add;
        private OperationStage operationStage = OperationStage.EnterFirstOperand;

        public void SetOperationMode(OperationMode operationMode)
        {
            this.operationMode = operationMode;

            if (operationStage == OperationStage.EnterFirstOperand)
            {
                SecondOperand.Reset();
                operationStage = OperationStage.EnterSecondOperand;
            }
            else if (operationStage == OperationStage.EnterSecondOperand)
            {
                CalculateResult();
                MoveResultToFirstOperand();
                SecondOperand.Reset();
                operationStage=OperationStage.EnterSecondOperand;
            }
            else if (operationStage == OperationStage.DisplayResult)
            {
                MoveResultToFirstOperand();
                SecondOperand.Reset();
                operationStage = OperationStage.EnterSecondOperand;
            }
        }

        public void CalculateResult()
        {
            operationStage = OperationStage.DisplayResult;
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
            Result.UpdateTextFromValue();
        }

        public void ModifyOperand(string commandString)
        {
            if (operationStage == OperationStage.DisplayResult)
            {
                MoveResultToFirstOperand();
            }

            switch (commandString)
            {
                case ".":
                    {
                        GetCurrentOperandObject().HasAPoint = true;
                        break;
                    }
                case "%":
                    {
                        GetCurrentOperandObject().DivideBy100();
                        break;
                    }
                case "+/-":
                    {
                        GetCurrentOperandObject().Invert();
                        break;
                    }
                case "C":
                    {
                        FirstOperand.Reset();
                        SecondOperand.Reset();
                        Result.Reset();
                        operationStage = OperationStage.EnterFirstOperand;
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

        public void MoveResultToFirstOperand()
        {
            FirstOperand.Value = Result.Value;
            FirstOperand.UpdateTextFromValue();
            FirstOperand.HasAPoint = false;
        }

    }
}
