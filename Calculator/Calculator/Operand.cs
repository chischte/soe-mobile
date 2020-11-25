using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Operand : IOperand
    {
        public double Value { get; private set; } = 0;

        public string Text { get; private set; } = "0";

        private bool HasAPoint { get; set; } = false;

        public void SetHasAPoint(bool hasAPoint)
        {
            HasAPoint = hasAPoint;
        }

        public void SetValue(double value)
        {
            Value = value;
        }


        public void DivideBy100()
        {
            Value /= 100;
            UpdateTextFromValue();
        }

        public void Invert()
        {
            Value *= -1;
            UpdateTextFromValue();
        }

        public void UpdateTextFromValue()
        {
            Text = Value.ToString();
        }

        public void AddText(String text)
        {
            if (HasAPoint && !Text.Contains("."))
            {
                Text += ".";
            }

            if (Text == "0")
            {
                Text = "";
            }
            Text += text;
            Value = Convert.ToDouble(Text);
        }

        public void Reset()
        {
            Value = 0;
            HasAPoint = false;
            Text = "0";
        }
    }
}
