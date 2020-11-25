using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface ICalculator
    {

       //void Calculator(IOperand firstOperand, IOperand secondOperand, IOperand resultOperand);

        //// Hier keine getter setter!
        //Operand FirstOperand { get; set; }
        //Operand SecondOperand { get; set; }
        //Operand ResultOperand { get; set; }
        //Operand GetCurrentOperandObject();
        
        void SetOperationMode(OperationMode operationMode);
        void CalculateResult();
        void ModifyOperand(string operandString);

        string GetCurrentOperandText();
        double GetCurrentOperandValue();
        string GetResultText();

        void AddStringToCurrentOperand(String number);
    

        // Diese 4 Funktionen sollen  versteckt sein, nicht im Interface
        // Bei Umbertos Lösung schauen wie er das mit private handhabt (verstecken der Funktionen)
        double Add();
        double Subtract();
        double Multiply();
        double Divide();
    }
}
