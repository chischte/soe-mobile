﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface ICalculator
    {
        Operand FirstOperand { get; set; }
        Operand SecondOperand { get; set; }
        Operand Result { get; set; }
        double Add();
        double Subtract();
        double Multiply();
        double Divide();
    }
}