using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Calculator
{


    public class Calculator : ICalculator
    {
        private Operand FirstOperand { get; set; }
        private Operand SecondOperand { get; set; }
        private Operand ResultOperand { get; set; }

        public Calculator()
        {
            FirstOperand = new Operand();
            SecondOperand = new Operand();
            ResultOperand = new Operand();
        }

        private OperationMode operationMode = OperationMode.Add;
        private OperationStage operationStage = OperationStage.EnterFirstOperand;



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
                        return ResultOperand;
                    }
                default:
                    {
                        return FirstOperand;
                    }
            }
        }

        public string GetCurrentOperandText()
        {
            return GetCurrentOperandObject().Text;

        }

        public double GetCurrentOperandValue()
        {
            return GetCurrentOperandObject().Value;

        }

        public string GetResultText()
        {
            return GetCurrentOperandObject().Text;

        }

        public void AddStringToCurrentOperand(string number)
        {
            GetCurrentOperandObject().AddText(number);
        }

        public void SetOperationMode(OperationMode operationMode)
        {
            this.operationMode = operationMode;
            SwitchValues();
        }

        private void SwitchValues()
        {
            switch (operationStage)
            {
                case OperationStage.EnterFirstOperand:
                    {
                        SecondOperand.Reset();
                        operationStage = OperationStage.EnterSecondOperand;
                        break;
                    }
                case OperationStage.EnterSecondOperand:
                    {
                        CalculateResult();
                        MoveResultToFirstOperand();
                        SecondOperand.Reset();
                        operationStage = OperationStage.EnterSecondOperand;
                        break;
                    }
                case OperationStage.DisplayResult:
                    {
                        MoveResultToFirstOperand();
                        SecondOperand.Reset();
                        operationStage = OperationStage.EnterSecondOperand;
                        break;
                    }
            }
        }

        private void MoveResultToFirstOperand()
        {
            FirstOperand.SetValue(ResultOperand.Value);
            FirstOperand.UpdateTextFromValue();
            FirstOperand.SetHasAPoint(false);
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
            ResultOperand.UpdateTextFromValue();
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
                        GetCurrentOperandObject().SetHasAPoint(true);
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
                        ResultOperand.Reset();
                        operationStage = OperationStage.EnterFirstOperand;
                        break;
                    }
            }
        }

        public double Add()
        {
            ResultOperand.SetValue(FirstOperand.Value + SecondOperand.Value);
            return ResultOperand.Value;
        }

        public double Subtract()
        {
            ResultOperand.SetValue(FirstOperand.Value - SecondOperand.Value);
            return ResultOperand.Value;
        }
        public double Multiply()
        {
            ResultOperand.SetValue(FirstOperand.Value * SecondOperand.Value);
            return ResultOperand.Value;
        }
        public double Divide()
        {
            ResultOperand.SetValue(FirstOperand.Value / SecondOperand.Value);
            return ResultOperand.Value;
        }
    }
}
