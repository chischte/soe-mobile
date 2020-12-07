using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface ICalculator
    {
        void SetOperationMode(OperationMode operationMode);
        double CalculateResult();
        string GetResultText();
        string GetCurrentOperandText();
        double GetCurrentOperandValue();
        void ModifyOperand(string operandString);
        void AddStringToCurrentOperand(String number);
    }
}
