using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
   public class Calculator:ICalculator
    {
        double firstOperand = 1.55;
        double secondOperand= 1.67;

        public double GetResult()
        {
            double result = firstOperand + secondOperand;
            return result;
        }


    }
}
