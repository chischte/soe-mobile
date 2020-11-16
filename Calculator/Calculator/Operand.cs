using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Operand
    {
        public double Value { get; set; } = 0;

        public string Text { get; set; } = "0";

        public bool HasAPoint { get; set; } = false;

        public void DivideBy100()
        {
            Value /= 100;
            Text = Value.ToString();
        }

        public void Invert()
        {
            Value *= -1;
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
