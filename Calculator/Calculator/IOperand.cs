using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Calculator
{
    public interface IOperand
    {
        void DivideBy100();
        void Invert();

        void UpdateTextFromValue();

        void AddText(String text);

        void Reset();
    }
}
