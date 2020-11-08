using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public interface ICalculator
    {
        Operand FirstOperand { get; set; }
        Operand SecondOperand { get; set; }
        double Result { get; set; }
        
        double Add();
        double Subtract();
        double Multiply();
        double Divide();
    }
}
