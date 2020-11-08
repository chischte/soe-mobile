using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public interface ICalculator
    {
        double FirstOperand { get; set; }
        double SecondOperand { get; set; }
        double Add(double a, double b);
        double Subtract(double a, double b);
        double Multiply(double a, double b);
        double Divide(double a, double b);
    }
}
